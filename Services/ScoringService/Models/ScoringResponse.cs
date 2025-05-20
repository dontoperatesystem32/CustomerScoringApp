namespace ScoringSystem_web_api.Services.ScoringService.Models
{
    public class ScoringResponse
    {
        public bool ScoringPassed { get; set; }
        public Dictionary<string, bool>? ScoringResults { get; set; }
        public decimal? OptionalAmount { get; set; }
    }
}
