using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.ConditionModels;
using ScoringSystem_web_api.Models.CustomerModels;

namespace ScoringSystem_web_api.Interfaces
{
    public interface ICustomerRepository
    {
        ICollection<Customer> GetCustomers();

        bool CustomerExists(int customerId);

        //bool CreateCondition(BaseCondition condition);
        bool CreateCustomer(Customer customerCreate);
        bool Save();
        bool UpdateCustomer(Customer customerUpdate);

        ICollection<Account> GetAccounts(Customer customer);

        float GetTotalLoans(Customer customer);


    }
}
