using BankingWebAPI.Repository.Entities;

namespace BankingWebAPI.Repository.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int AccountId { get; set; }        // FK to Accounts
        public DateTime Date { get; set; }        // required by DB
        public decimal Amount { get; set; }
        public int Duration { get; set; }         // months
        public decimal Payments { get; set; }     // required by DB
        public string Status { get; set; } = null!;
    }
}
