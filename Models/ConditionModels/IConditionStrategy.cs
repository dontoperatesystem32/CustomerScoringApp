using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.CustomerModels;

namespace ScoringSystem_web_api.Models.ConditionModels
{
    public interface IConditionStrategy
    {
        public int Id { get; set; }
        public string ConditionType { get; set; }
        public bool IsEnabled { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public abstract bool EvaluateCustomer(Customer customer);
    }
}
