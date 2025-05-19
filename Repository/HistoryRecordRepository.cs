using ScoringSystem_web_api.Data;
using ScoringSystem_web_api.Interfaces;
using ScoringSystem_web_api.Models.AuditModels;

namespace ScoringSystem_web_api.Repository
{
    public class HistoryRecordRepository : IHistoryRecordRepository
    {
        private readonly DataContext _context;
        public HistoryRecordRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateHistoryRecord(ScoringDetails historyRecord)
        {
            _context.ScoringHistory.Add(historyRecord);
            return Save();
        }
        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
    
    
}
