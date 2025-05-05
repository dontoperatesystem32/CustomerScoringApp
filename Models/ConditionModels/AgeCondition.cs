using System.Text.Json;

namespace ScoringSystem_web_api.Models.ConditionModels
{
    public class AgeCondition : BaseCondition
    {
        //public string? PropertiesJson { get; set; }
        public AgeCondition() : base()
        {
            Properties = new Dictionary<string, object>
                {
                    { "MinimalAge", 18 }
                };
            ConditionType = "AgeCondition";
            IsEnabled = true;
        }
        public AgeCondition(Dictionary<string, object> properties = null)
        {
            properties ??= new Dictionary<string, object>
                {
                    { "MinimalAge", 18 }
                };


            Properties = properties;
            ConditionType = "AgeCondition";
        }
        public override bool EvaluateCustomer(Customer customer)
        {
            if(customer.Age < (int)Properties["MinimalAge"]) return false;
            return true;
        }



    }
}
