using ScoringSystem_web_api.Data;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models.ConditionModels;

namespace ScoringSystem_web_api.Repository
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly DataContext _context;
        public ConditionRepository(DataContext context)
        {
               _context = context;
        }

        public ICollection<BaseCondition> GetConditions()
        {
            return _context.ConditionStrategies.OrderBy(p => p.Id).ToList();
        }
    }
}
