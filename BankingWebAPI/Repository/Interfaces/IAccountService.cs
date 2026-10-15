using BankingWebAPI.Repository.DTO;

namespace BankingWebAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAccountsByCustomerIdAsync(int customerId);
        Task CreateAccountAsync(int customerId, int accountTypeId, decimal initialDeposit);
        Task CreateDefaultAccountAsync(int customerId);
        Task DepositToAccountAsync(int accountId, decimal amount);
    }
}
