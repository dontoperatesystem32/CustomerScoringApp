using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Services.ScoringService.Models;

namespace ScoringSystem_web_api.Services.Abstraction
{
    public interface IScoringService
    {
        ServiceResult<ScoringResponse> EvaluateCustomer(Customer customer);
    }
}
