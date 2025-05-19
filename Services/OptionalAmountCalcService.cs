using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models.CustomerModels;

namespace ScoringSystem_web_api.Services
{
    public class OptionalAmountCalcService : IOptionalAmountCalcService
    {
        private const float MonthlyInterestRate = 0.015f;
        public readonly ICustomerRepository _customerRepository;

        public OptionalAmountCalcService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public decimal? CalcMaximumLoan(Customer customer)
        {
            // Step 1: Calculate monthly income

            // Step 2: Calculate total monthly loan payments

            float totalLoanPayments = _customerRepository.GetTotalLoans(customer);


            // Step 3: Calculate available monthly payment capacity (using DTI)
            float DTI_Cap = 0.40f;  // Max 40% of monthly income
            float availableMonthlyPayment = Math.Max((DTI_Cap * customer.Salary) - totalLoanPayments, 0);

            if (availableMonthlyPayment == 0)
            {
                return null;  // No capacity for additional loans
            }

            // Step 4: Determine loan term based on age
            int termYears = (customer.Age <= 40) ? 25 : (customer.Age <= 55) ? 15 : 10;
            int termMonths = termYears * 12;

            // Step 5: Calculate max loan amount (present value of annuity)
            decimal maxLoanAmount = (decimal)(availableMonthlyPayment * ((1 - Math.Pow(1 + MonthlyInterestRate, -termMonths)) / MonthlyInterestRate));

            return maxLoanAmount;
        }
    }
    
}
