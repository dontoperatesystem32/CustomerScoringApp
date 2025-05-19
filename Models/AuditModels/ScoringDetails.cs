using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Dto;

namespace ScoringSystem_web_api.Models.AuditModels
{
    public class ScoringDetails
    {
        //customerDto fields are already present here bc of inheritance
        
        //scoring request id
        public int Id { get; set; }
        //customer details
        public Customer EvaluatedCustomer { get; set; }
        //scoring details
        public DateTime CreatedAt { get; set; }
        public bool ScoringPassed { get; set; }
        public decimal? OptionalAmount { get; set; }

    }
}
