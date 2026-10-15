namespace BankingWebAPI.Repository.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } = null!;      // "Credit" or "Debit" (DB constraint)
        public string Operation { get; set; } = null!; // e.g., "Transfer", "Deposit", "Loan"
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }           // balance after transaction

        // Relationships
        public Account Account { get; set; } = null!;
    }
}
