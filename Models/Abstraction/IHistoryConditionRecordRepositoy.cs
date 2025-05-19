using System.Runtime.InteropServices;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.ConditionModels;
using ScoringSystem_web_api.Models.CustomerModels;
using ScoringSystem_web_api.Models.AuditModels;

namespace ScoringSystem_web_api.Models.Abstraction
{
    public interface IHistoryConditionRecordRepository
    {
        public bool CreateHistoryConditionRecord(ScoringConditionDetails historyRecord);

        bool Save();

    }
}
