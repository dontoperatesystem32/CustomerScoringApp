using static System.Net.Mime.MediaTypeNames;

namespace ScoringSystem_web_api.Models.ConditionModels
{
    public class SalaryCondition : BaseCondition
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public string ConditionType { get; set; }

        public Dictionary<string, object> Properties { get; set; }
        public string PropertiesJson { get; set; }
        public SalaryCondition() { }
        public SalaryCondition(float minSalary = 1000)
        {
            Properties = new Dictionary<string, object>();
            Properties["MinimalSalary"] = minSalary;
            ConditionType = "SalaryCondition";
        }
        public override bool EvaluateCustomer(Customer customer)
        {
            if (customer.Salary < (float)Properties["MinimalSalary"]) return false;
            return true;
        }

        public float OptionalAmount(Customer customer, bool salaryEligible)
        {
            if (!salaryEligible) return 0;
            return 12 * (customer.Salary);
        }
    }
}
