namespace ScoringSystem_web_api.Models.ConditionModels
{
    public abstract class BaseCondition : IConditionStrategy
    {
        public int Id { get; set; }
        public string ConditionType { get; set; }
        public bool IsEnabled { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public abstract bool EvaluateCustomer(Customer customer);
    }
}
