using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingWebAPI.Repository.Repo
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly BankDbContext _dbContext;
        public TransactionRepo(BankDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _dbContext.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
