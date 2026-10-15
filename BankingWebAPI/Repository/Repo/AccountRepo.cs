using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingWebAPI.Repository.Repo
{
    public class AccountRepo : IAccountRepo
    {
        private readonly BankDbContext _dbContext;
        public AccountRepo(BankDbContext dbContext) => _dbContext = dbContext;

        public async Task<int> CreateAccountForCustomerAsync(int customerId, int accountTypeId, decimal initialDeposit)
        {
            // Accounts.Frequency has a CHECK constraint → must be one of: "Monthly" | "Weekly" | "AfterTransaction"
            var account = new Account
            {
                Frequency = "Monthly",
                Created = DateTime.UtcNow.Date,
                Balance = initialDeposit,
                AccountTypeId = accountTypeId   // <-- use AccountTypeId (mapped to DB column AccountTypesId)
            };

            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();

            // Link account to customer through Dispositions
            var disp = new Disposition
            {
                CustomerId = customerId,
                AccountId = account.AccountId,
                Type = "OWNER"           // "OWNER" | "DISPONENT"
            };
            await _dbContext.Dispositions.AddAsync(disp);
            await _dbContext.SaveChangesAsync();

            return account.AccountId;
        }

        public async Task<IEnumerable<Account>> GetAccountsForCustomerAsync(int customerId)
        {
            // Join via Dispositions (DB truth for Customer↔Account)
            return await _dbContext.Dispositions
                .AsNoTracking()
                .Where(d => d.CustomerId == customerId)
                .Select(d => d.Account)
                .ToListAsync();
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId) =>
            await _dbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AccountId == accountId);

        public async Task AddAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}
