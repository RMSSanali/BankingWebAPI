namespace BankingWebAPI.Repository.Entities
{
    public class Disposition
    {
        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; } = null!;

        // Relationships
        public Customer Customer { get; set; } = null!;
        public Account Account { get; set; } = null!;
        public ICollection<Card> Cards { get; set; } = new List<Card>();

        public Disposition()
        {
            
        }
        public Disposition(int dispositionId, int customerId, int accountId, string type, Customer customer, Account account, ICollection<Card> cards)
        {
            DispositionId = dispositionId;
            CustomerId = customerId;
            AccountId = accountId;
            Type = type;
            Customer = customer;
            Account = account;
            Cards = cards;
        }
    }
}
