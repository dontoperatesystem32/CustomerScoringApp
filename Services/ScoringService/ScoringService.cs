using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Models.ConditionModels;
using ScoringSystem_web_api.Models.AuditModels;
using ScoringSystem_web_api.Services.Abstraction;
using ScoringSystem_web_api.Models.Abstraction;
using ScoringSystem_web_api.Services.ScoringService.Models;

using Prometheus;

namespace ScoringSystem_web_api.Services.ScoringService
{
    public class ScoringService : IScoringService
    {
        private readonly IConditionRepository _conditionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHistoryRecordRepository _historyRecordRepository;
        private readonly IHistoryConditionRecordRepository _historyConditionRecordRepository;
        
        private static readonly Histogram ConditionEvalTime = Metrics
            .CreateHistogram("condition_evaluation_duration_seconds", "Duration of each condition", new HistogramConfiguration
            {
                LabelNames = new[] { "condition" }
            });
        
        private static readonly Histogram TotalProcessingTime = Metrics
            .CreateHistogram("total_processing_duration_seconds", "Total time for all conditions");
        
        public ScoringService(
            IConditionRepository conditionrepository,
            ICustomerRepository customerRepository,
            IHistoryRecordRepository historyRecordRepository,
            IHistoryConditionRecordRepository historyConditionRecordRepository
        )
        {
            _conditionRepository = conditionrepository;
            _customerRepository = customerRepository;
            _historyRecordRepository = historyRecordRepository;
            _historyConditionRecordRepository = historyConditionRecordRepository;
        }

        public ServiceResult<ScoringResponse> EvaluateCustomer(Customer customer)
        {

            try
            {
                
                    //get all active connections
                    var conditions = _conditionRepository.GetActiveConditions();

                    if (!conditions.Any())
                        return ServiceResult<ScoringResponse>.Fail("No active conditions found.");

                    //the customer to the db
                    if (!_customerRepository.CreateCustomer(customer))
                    {
                        return ServiceResult<ScoringResponse>.Fail("Could not create a customer");
                    }

                    //define dictionary with scoring results
                    var scoringResults = new Dictionary<string, bool>();

                    bool conditionMet;
                    bool scoringPassed = true;

                    using (TotalProcessingTime.NewTimer())
                    {
                        //evaluate each condition
                        foreach (var condition in conditions)
                        {
                            using (ConditionEvalTime.WithLabels(condition.ConditionType).NewTimer())
                            {
                                conditionMet = condition.EvaluateCustomer(customer);
                                scoringResults.Add(condition.ConditionType, conditionMet);
                            }

                            if (!conditionMet)
                                scoringPassed = false;
                        }
                    }

                    if (!scoringResults.Any())
                        return ServiceResult<ScoringResponse>.Fail(
                            "Something went wrong when evaluating by conditions.");

                    //propose optional amount if customer passed the scoring

                    decimal? optionalAmount = null;
                    //if (scoringPassed)
                    //{
                    //    optionalAmount = _optionalAmountCalcService.CalcMaximumLoan(customer);
                    //}

                    //List<decimal> proposedAmounts = new List<decimal>();
                    Dictionary<string, decimal> proposedAmounts = new Dictionary<string, decimal>();

                    if (scoringPassed)
                    {
                        decimal tempOptionalAmount;
                        foreach (BaseCondition condition in conditions)
                        {
                            tempOptionalAmount = condition.OptionalAmount(customer);

                            proposedAmounts.Add(condition.ConditionType, tempOptionalAmount);
                        }

                        List<decimal> proposedAmountsList = new List<decimal>();

                        foreach (var proposition in proposedAmounts)
                        {
                            if (!(proposition.Value == -1))
                            {
                                proposedAmountsList.Add(proposition.Value);
                            }
                        }

                        if (proposedAmountsList.Count > 0)
                        {
                            optionalAmount = proposedAmountsList.Min();
                        }
                        else
                        {
                            optionalAmount = -1;
                        }
                    }
                    else
                    {
                        optionalAmount = -1;
                    }


                    //add scoring results to the history
                    var scoringHistoryRecord = new ScoringDetails()
                    {
                        EvaluatedCustomer = customer,
                        ScoringPassed = scoringPassed,
                        CreatedAt = DateTime.Now,
                        OptionalAmount = optionalAmount
                    };

                    //add to db
                    if (!_historyRecordRepository.CreateHistoryRecord(scoringHistoryRecord))
                    {
                        return ServiceResult<ScoringResponse>.Fail("Could not add scoring record to history.");
                    }

                    //create individual condition evaluation results

                    foreach (BaseCondition condition in conditions)
                    {
                        var historyConditionRecord = new ScoringConditionDetails()
                        {
                            ScoringRequest = scoringHistoryRecord,
                            EvaluatedCondition = condition,
                            EvaluationResult = scoringResults[condition.ConditionType],
                            OptionalAmount = scoringPassed ? proposedAmounts[condition.ConditionType] : -1,
                            //OptionalAmount = 0,
                            CreatedAt = DateTime.Now
                        };
                        if (!_historyConditionRecordRepository.CreateHistoryConditionRecord(historyConditionRecord))
                        {
                            return ServiceResult<ScoringResponse>.Fail(
                                "Could not add individual condition details to history.");
                        }
                    }


                    //send response
                    var response = new ScoringResponse()
                    {
                        ScoringPassed = scoringPassed,
                        ScoringResults = scoringResults,
                        OptionalAmount = optionalAmount
                    };


                    return ServiceResult<ScoringResponse>.Ok(response);

                    
            }
            catch (Exception ex)
            {
                // Log exception if needed
                return ServiceResult<ScoringResponse>.Fail("Scoring failed: " + ex.Message);
            }
        }
    }
}
