using ScoringSystem_web_api.Data;
using ScoringSystem_web_api.Models.Abstraction;
using ScoringSystem_web_api.Models.AuditModels;

namespace ScoringSystem_web_api.Repository
{
    public class HistoryConditionRecordRepository : IHistoryConditionRecordRepository
    {
        private readonly DataContext _context;
        public HistoryConditionRecordRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateHistoryConditionRecord(ScoringConditionDetails historyRecord)
        {
            _context.ScoringConditionHistory.Add(historyRecord);
            return Save();
        }
        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
    
}

