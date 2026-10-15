using BankingWebAPI.Repository.DTO;

namespace BankingWebAPI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> GetTransactionsByAccountIdAsync(int accountId);
        Task TransferBetweenAccountsAsync(int fromAccountId, int toAccountId, decimal amount);
        Task TransferToAnotherCustomerAsync(int fromAccountId, int toAccountNumber, decimal amount);
    }
}
