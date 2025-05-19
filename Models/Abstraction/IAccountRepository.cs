using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.ConditionModels;
using ScoringSystem_web_api.Models.CustomerModels;

namespace ScoringSystem_web_api.Models.Abstraction
{
    public interface IAccountRepository
    {
        Account GetAccount(int id);
        bool Save();
    }
}
