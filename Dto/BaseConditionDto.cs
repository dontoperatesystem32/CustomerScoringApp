
namespace ScoringSystem_web_api.Dto
{
    public class BaseConditionDto
    {
        //public int? Id { get; set; }
        public bool? IsEnabled { get; set; }
        public string? ConditionType { get; set; }
        public Dictionary<string, object>? Properties { get; set; }
    }
}
