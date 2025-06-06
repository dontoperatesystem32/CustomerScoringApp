﻿using AutoMapper;
using ScoringSystem_web_api.Data;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Models.ConditionModels;
using ScoringSystem_web_api.Models.Abstraction;

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

        public ICollection<BaseCondition> GetActiveConditions()
        {
            return _context.ConditionStrategies.Where(p => p.IsEnabled).OrderBy(p => p.Id).ToList();
        }

        public bool ConditionExists(int conditionId)
        {
            return _context.ConditionStrategies.Any(с => с.Id == conditionId);
        }

        public bool ConditionExists(string conditionType)
        {
            return _context.ConditionStrategies.Any(с => с.ConditionType == conditionType);
        }

        //public bool CreateCondition(BaseCondition condition)
        //{
        //    Type targetType = Type.GetType(condition.ConditionType);
        //    var conditionMap = _mapper.Map(condition, condition.GetType(), targetType);
        //    _context.Add(condition);

        //    return Save();
        //}

        public bool CreateCondition(string conditionType)
        {
            if (ConditionExists(conditionType))
            {
                return false;
            }

            conditionType = "ScoringSystem_web_api.Models.ConditionModels." + conditionType;

            var condition = Activator.CreateInstance(Type.GetType(conditionType)) as BaseCondition;

            _context.Add(condition);

            return Save();
        }

        public bool UpdateCondition(BaseCondition condition)
        {
            _context.Update(condition);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
