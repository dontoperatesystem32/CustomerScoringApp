using ScoringSystem_web_api.Models.CustomerModels;
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
            //int Age = DeserializeInt((JsonElement)Properties["MinimalAge"]);
            DeserializeProperties();
            if (customer.Age < (int)Properties["MinimalAge"]) return false;
            return true;
            //if (!Properties.TryGetValue("MinimalAge", out object val))
            //    throw new Exception();
            //if (customer.Age < (int)val) return false;
            //return true;
        }

        public override decimal OptionalAmount(Customer customer)
        {
            //dont give loans more than 5000 if the customer is under 18 or over 50
            //as they most probably are not able to pay it back
            if (customer.Age<18 || customer.Age>50)
            {
                return 5000;
            }
            //in other cases age doesnt matter
            return -1;
        }


        //TODO : CREATE METHOD TO DESERELIZE JSON FROM DB
        public int DeserializeInt(JsonElement property)
        {
            return property.GetInt32();
        }
        public void DeserializeProperties()
        {
            var minAge = Properties["MinimalAge"];
            minAge = DeserializeInt((JsonElement)minAge);
            Properties["MinimalAge"] = minAge;
        }




    }
}
