using BankingWebAPI.Repository.DTO;
using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using BankingWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BankingWebAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly BankDbContext _dbContext;

        public TransactionService(ITransactionRepo transactionRepo, IAccountRepo accountRepo, BankDbContext dbContext)
        {
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByAccountIdAsync(int accountId)
        {
            var transactions = await _transactionRepo.GetTransactionsByAccountIdAsync(accountId);

            return transactions.Select(t => new TransactionDTO
            {
                TransactionId = t.TransactionId,
                Date = t.Date,
                Type = t.Type,
                Operation = t.Operation,
                Amount = t.Amount,
                Balance = t.Balance
            });
        }

        public async Task TransferBetweenAccountsAsync(int fromAccountId, int toAccountId, decimal amount)
        {
            ValidateTransfer(fromAccountId, toAccountId, amount);

            var fromAccount = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == fromAccountId);
            var toAccount = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.AccountId == toAccountId);

            if (fromAccount == null || toAccount == null)
                throw new InvalidOperationException("Account not found.");

            if (fromAccount.Balance < amount)
                throw new InvalidOperationException("Insufficient balance.");

            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            _dbContext.Transactions.Add(new Transaction
            {
                AccountId = fromAccountId,
                Date = DateTime.UtcNow.Date,
                Type = "Debit",
                Operation = "Transfer",
                Amount = amount,
                Balance = fromAccount.Balance
            });

            _dbContext.Transactions.Add(new Transaction
            {
                AccountId = toAccountId,
                Date = DateTime.UtcNow.Date,
                Type = "Credit",
                Operation = "Transfer",
                Amount = amount,
                Balance = toAccount.Balance
            });

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task TransferToAnotherCustomerAsync(int fromAccountId, int toAccountNumber, decimal amount)
        {
            ValidateTransfer(fromAccountId, toAccountNumber, amount);
            await TransferBetweenAccountsAsync(fromAccountId, toAccountNumber, amount);
        }

        private static void ValidateTransfer(int fromAccountId, int toAccountId, decimal amount)
        {
            if (fromAccountId <= 0 || toAccountId <= 0)
                throw new ArgumentException("Account IDs must be greater than zero.");
            if (fromAccountId == toAccountId)
                throw new ArgumentException("Source and destination accounts must be different.");
            if (amount <= 0)
                throw new ArgumentException("Transfer amount must be greater than zero.");
        }
    }
}
