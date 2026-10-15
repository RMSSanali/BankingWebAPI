namespace BankingWebAPI.Repository.DTO
{
    public class LoanDTO
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; } // Duration in months
    }
}
