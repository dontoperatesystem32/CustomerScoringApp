using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Models.ConditionModels;
using ScoringSystem_web_api.Models.AuditModels;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoringSystem_web_api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ScoringController : Controller
    {
        private readonly IOptionalAmountCalcService _optionalAmountCalcService;
        private readonly IConditionRepository _conditionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHistoryRecordRepository _historyRecordRepository;
        private readonly IHistoryConditionRecordRepository _historyConditionRecordRepository;
        private readonly IMapper _mapper;
        public ScoringController(
            IOptionalAmountCalcService optionalAmountCalcService,
            IConditionRepository conditionrepository,
            ICustomerRepository customerRepository,
            IHistoryRecordRepository historyRecordRepository,
            IHistoryConditionRecordRepository historyConditionRecordRepository,
            IMapper mapper
        ){
            _optionalAmountCalcService = optionalAmountCalcService;
            _conditionRepository = conditionrepository;
            _customerRepository = customerRepository;
            _historyRecordRepository = historyRecordRepository;
            _historyConditionRecordRepository = historyConditionRecordRepository;
            _mapper = mapper;

        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult EvaluateCustomer([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var conditions = _conditionRepository.GetConditions();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!conditions.Any())
                return BadRequest(ModelState);

            //assign accounts to the customer


            var accounts = _mapper.Map<ICollection<Account>>(customerDto.Accounts);
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Accounts = accounts;

            //add newly create customer to the db


            if (!_customerRepository.CreateCustomer(customer))
            {
                return BadRequest(ModelState);
            }


            //define dictionary with scoring results
            var scoringResults = new Dictionary<string, bool>();

            bool conditionMet;
            bool scoringPassed = true;

            //evaluate each condition
            foreach (var condition in conditions)
            {
                conditionMet = condition.EvaluateCustomer(customer);
                scoringResults.Add(condition.ConditionType, conditionMet);
                if (!conditionMet)
                    scoringPassed = false;
            }

            //propose optional amount if customer passed the scoring
            
            if (!scoringResults.Any())
                return BadRequest(ModelState);


            decimal? optionalAmount = null;
            if (scoringPassed)
            {
                optionalAmount = _optionalAmountCalcService.CalcMaximumLoan(customer);
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
                return BadRequest(ModelState);
            }

            //create individual condition evaluation results

            foreach(BaseCondition condition in conditions)
            {
                var historyConditionRecord = new ScoringConditionDetails()
                {
                    ScoringRequest = scoringHistoryRecord,
                    EvaluatedCondition = condition,
                    EvaluationResult = scoringResults[condition.ConditionType],
                    CreatedAt = DateTime.Now
                };
                if (!_historyConditionRecordRepository.CreateHistoryConditionRecord(historyConditionRecord))
                {
                    return BadRequest(ModelState);
                }
            }


            //send response
            var response = new
            {
                ScoringPassed = scoringPassed,
                ScoringResults = scoringResults,
                OptionalAmount = optionalAmount
            };


            return Ok(response);
        }

    }
}
