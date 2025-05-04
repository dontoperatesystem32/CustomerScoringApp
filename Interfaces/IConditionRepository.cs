using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.ConditionModels;
namespace ScoringSystem_web_api.Interfaces
{
    public interface IConditionRepository
    {
        ICollection<BaseCondition> GetConditions();
        //BaseCondition GetCondition(int id);
        bool ConditionExists(int conditionId);


    }
}
