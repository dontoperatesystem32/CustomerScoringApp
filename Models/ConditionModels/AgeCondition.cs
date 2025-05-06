using System.Text.Json;

namespace ScoringSystem_web_api.Models.ConditionModels
{
    public class AgeCondition : BaseCondition
    {
        //public string? PropertiesJson { get; set; }
        public AgeCondition() : base()
        {
            Properties.Add("MinimalAge", 18);
            ConditionType = "AgeCondition";
            IsEnabled = true;
        }
        //public AgeCondition(Dictionary<string, object> properties = null)
        //{
        //    properties ??= new Dictionary<string, object>
        //        {
        //            { "MinimalAge", 18 }
        //        };


        //    Properties = properties;
        //    ConditionType = "AgeCondition";
        //}
        public override bool EvaluateCustomer(Customer customer)
        {
            // TODO: CALL THE METHOD HERE
            if (!Properties.TryGetValue("MinimalAge", out object val))
                throw new Exception();
            if (customer.Age < (int)val) return false;
            return true;
        }

        //TODO : CREATE METHOD TO DESERELIZE JSON FROM DB



    }
}
