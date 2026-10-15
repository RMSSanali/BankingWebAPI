using BankingWebAPI.Repository.Entities;

namespace BankingWebAPI.Repository.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!; // e.g., "Admin", "Customer"
        public int? CustomerId { get; set; }
    }
}
