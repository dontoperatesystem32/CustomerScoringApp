using System.Text.Json;

namespace ScoringSystem_web_api.Models.ConditionModels
{
    public class AgeCondition : BaseCondition
    {
        public int Id { get; set; }
        public string ConditionType { get; set; }
        public bool IsEnabled { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public string? PropertiesJson { get; set; }
        public AgeCondition() { }
        public AgeCondition(int minAge = 18)
        {
            Properties = new Dictionary<string, object>();
            Properties["MinimalAge"] = minAge;

            ConditionType = "AgeCondition";
        }
        public override bool EvaluateCustomer(Customer customer)
        {
            if(customer.Age < (int)Properties["MinimalAge"]) return false;
            return true;
        }



    }
}
