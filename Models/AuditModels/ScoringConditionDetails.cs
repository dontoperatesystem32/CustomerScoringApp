using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Models.ConditionModels;

namespace ScoringSystem_web_api.Models.AuditModels
{
    public class ScoringConditionDetails
    {
        //customerDto fields are already present here bc of inheritance
        
        //scoring request id
        public int Id { get; set; }
        //customer details
        public ScoringDetails ScoringRequest { get; set; }
        //condition
        public BaseCondition EvaluatedCondition { get; set; }
        public bool EvaluationResult { get; set; }
        public decimal OptionalAmount { get; set; }

        //timestamp
        public DateTime CreatedAt { get; set; }

    }
}

