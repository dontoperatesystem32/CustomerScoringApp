namespace ScoringSystem_web_api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public float Salary { get; set; }

        public ICollection<Account>? Accounts { get; set; }

    }
}
