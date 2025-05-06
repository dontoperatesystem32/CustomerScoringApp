
using ScoringSystem_web_api.Models;

namespace ScoringSystem_web_api.Dto
{
    public class CustomerDto
    {
        //public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public float Salary { get; set; }

        public ICollection<AccountDto>? Accounts { get; set; }
    }
}
