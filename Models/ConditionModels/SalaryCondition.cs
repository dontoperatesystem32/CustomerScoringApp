using ScoringSystem_web_api.Models.CustomerModels;

using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ScoringSystem_web_api.Models.ConditionModels
{
    public class SalaryCondition : BaseCondition
    {
        public SalaryCondition() 
        {
            Properties = new Dictionary<string, object>();
            Properties["MinimalSalary"] = 1000;
            ConditionType = "SalaryCondition";
            IsEnabled = true;
        }
        public SalaryCondition(float minSalary = 1000)
        {
            Properties = new Dictionary<string, object>();
            Properties["MinimalSalary"] = minSalary;
            ConditionType = "SalaryCondition";
        }
        public override bool EvaluateCustomer(Customer customer)
        {
            DeserializeProperties();
            if (customer.Salary < (float)Properties["MinimalSalary"]) return false;
            return true;
        }

        public override decimal OptionalAmount(Customer customer)
        {
            return (decimal)customer.Salary * 10;
        }

        //public float OptionalAmount(Customer customer, bool salaryEligible)
        //{
        //    if (!salaryEligible) return 0;
        //    return 12 * (customer.Salary);
        //}

        public float DeserializeFloat(JsonElement property)
        {
            return property.GetSingle();
        }
        public void DeserializeProperties()
        {
            var minSalary = Properties["MinimalSalary"];
            minSalary = DeserializeFloat((JsonElement)minSalary);
            Properties["MinimalSalary"] = minSalary;
        }


    }
}
