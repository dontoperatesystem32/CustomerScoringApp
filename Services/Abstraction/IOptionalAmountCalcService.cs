using ScoringSystem_web_api.Models.CustomerModels;

namespace ScoringSystem_web_api.Services.Abstraction
{
    public interface IOptionalAmountCalcService
    {
        decimal? CalcMaximumLoan(Customer customer);
    }
}
