namespace ScoringSystem_web_api.Models.AuditModels
{
    public class ScoringHistory
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime Timestamp { get; set; }
        public float OptionalAmount { get; set; }

    }
}
