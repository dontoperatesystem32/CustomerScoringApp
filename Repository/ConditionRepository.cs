using AutoMapper;
using ScoringSystem_web_api.Data;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models.ConditionModels;

namespace ScoringSystem_web_api.Repository
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ConditionRepository(DataContext context)
        {
               _context = context;
        }

        public ICollection<BaseCondition> GetConditions()
        {
            return _context.ConditionStrategies.OrderBy(p => p.Id).ToList();
        }

        public bool ConditionExists(int conditionId)
        {
            return _context.ConditionStrategies.Any(с => с.Id == conditionId);
        }

        public bool CreateCondition(BaseCondition condition)
        {
            Type targetType = Type.GetType(condition.ConditionType);
            var conditionMap = _mapper.Map(condition, condition.GetType(), targetType);
            _context.Add(condition);

            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
