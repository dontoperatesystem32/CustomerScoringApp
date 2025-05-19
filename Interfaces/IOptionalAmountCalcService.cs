using ScoringSystem_web_api.Models.CustomerModels;

namespace ScoringSystem_web_api.Interfaces
{
    public interface IOptionalAmountCalcService
    {
        decimal? CalcMaximumLoan(Customer customer);
    }
}
