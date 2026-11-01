using BankingWebAPI.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAccountRepo
{
    Task<int> CreateAccountForCustomerAsync(int customerId, int accountTypeId, decimal initialDeposit);
    Task<IEnumerable<Account>> GetAccountsForCustomerAsync(int customerId);
    Task<Account?> GetAccountByIdAsync(int accountId);
    Task<bool> IsOwnedByCustomerAsync(int accountId, int customerId);
    Task AddAccountAsync(Account account);
    Task UpdateAccountAsync(Account account);
}
