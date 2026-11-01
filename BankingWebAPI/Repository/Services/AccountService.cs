using BankingWebAPI.Repository.DTO;
using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using BankingWebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingWebAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        public AccountService(IAccountRepo accountRepo) => _accountRepo = accountRepo;

        public async Task<IEnumerable<AccountDTO>> GetAccountsByCustomerIdAsync(int customerId)
        {
            var accounts = await _accountRepo.GetAccountsForCustomerAsync(customerId);
            return accounts.Select(a => new AccountDTO
            {
                AccountId = a.AccountId,
                Balance = a.Balance,
                // ▶ use AccountTypeId (entity) — it’s mapped to DB column AccountTypesId via [Column]
                AccountTypeId = a.AccountTypeId ?? 0
            });
        }

        public async Task CreateAccountAsync(int customerId, int accountTypeId, decimal initialDeposit)
        {
            await _accountRepo.CreateAccountForCustomerAsync(customerId, accountTypeId, initialDeposit);
        }

        public async Task CreateDefaultAccountAsync(int customerId)
        {
            await _accountRepo.CreateAccountForCustomerAsync(customerId, accountTypeId: 1, initialDeposit: 0);
        }

        public async Task DepositToAccountAsync(int accountId, decimal amount)
        {
            var account = await _accountRepo.GetAccountByIdAsync(accountId)
                ?? throw new System.Exception("Account not found.");

            account.Balance += amount;
            await _accountRepo.UpdateAccountAsync(account);
        }

        public Task<bool> IsOwnedByCustomerAsync(int accountId, int customerId)
            => _accountRepo.IsOwnedByCustomerAsync(accountId, customerId);
    }
}
