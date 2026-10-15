namespace BankingWebAPI.Repository.Entities
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public string? Description { get; set; }

        // Just in case: Navigation property
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
