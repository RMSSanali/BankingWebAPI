namespace BankingWebAPI.Repository.DTO
{
    public class TransferToCustomerDTO
    {
        public int FromAccountId { get; set; }
        public int ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
