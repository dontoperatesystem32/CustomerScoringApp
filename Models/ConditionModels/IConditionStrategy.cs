using ScoringSystem_web_api.Models;

namespace ScoringSystem_web_api.Models.ConditionModels
{
    public interface IConditionStrategy
    {
        public bool IsEnabled { get; set; }
        public bool EvaluateCustomer(Customer customer);
    }
}
