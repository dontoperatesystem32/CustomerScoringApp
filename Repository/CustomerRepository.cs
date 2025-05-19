using AutoMapper;
using ScoringSystem_web_api.Data;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Models.ConditionModels;

namespace ScoringSystem_web_api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CustomerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ICollection<Customer> GetCustomers()
        {
            return _context.Customers.OrderBy(p => p.Id).ToList();
        }
        public ICollection<Account> GetAccounts(Customer customer)
        {
            return _context.Accounts.Where(a => a.Customer.Id == customer.Id).ToList();
        }
        public float GetTotalLoans(Customer customer)
        {
            float total = 0;
            var accounts = _context.Accounts.Where(a => a.Customer.Id == customer.Id).ToList();
            foreach (var account in accounts)
            {
                if (account.Loans != null)
                {
                    foreach (var loan in account.Loans)
                    {
                        total += loan;
                    }
                }
            }
            return total;
        }
        public bool CustomerExists(int customerId)
        {
            return _context.Customers.Any(c => c.Id == customerId);
        }
        public bool CreateCustomer(Customer customerCreate)
        {
            if (customerCreate == null)
            {
                return false;
            }
            _context.Add(customerCreate);
            return Save();
        }
        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customerUpdate)
        {
            _context.Update(customerUpdate);
            return Save();
        }


    }
}
