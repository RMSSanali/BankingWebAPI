namespace BankingWebAPI.Repository.DTO
{
    public class CreateAccountDTO
    {
        public int AccountTypeId { get; set; }
        public decimal InitialDeposit { get; set; }
    }
}
