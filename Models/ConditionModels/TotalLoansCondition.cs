using System.Text.Json;
using ScoringSystem_web_api.Models.CustomerModels;

namespace ScoringSystem_web_api.Models.ConditionModels
{
    public class TotalLoansCondition : BaseCondition
    {
        public TotalLoansCondition() : base()
        {
            Properties.Add("MaximalTotalLoans", (float)5000);
            ConditionType = "TotalLoansCondition";
            IsEnabled = true;
        }

        public override bool EvaluateCustomer(Customer customer)
        {
            DeserializeProperties();
            float totalLoans = 0;
            if (customer.Accounts != null)
            {
                foreach (var account in customer.Accounts)
                {
                    foreach (var loan in account.Loans)
                        totalLoans += loan;
                }
            }

            Console.WriteLine("total loans for customer " + customer.FirstName + " " + customer.LastName + ": " + totalLoans);

            if (totalLoans > (float)Properties["MaximalTotalLoans"]) return false;
            return true;
        }

        public float DeserializeFloat(JsonElement property)
        {
            return property.GetSingle();
        }
        public void DeserializeProperties()
        {
            var maxTotalLoans = Properties["MaximalTotalLoans"];
            maxTotalLoans = DeserializeFloat((JsonElement)maxTotalLoans);
            Properties["MaximalTotalLoans"] = maxTotalLoans;
        }
    }
}
