using BankingWebAPI.Repository.DTO;
using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using BankingWebAPI.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BankingWebAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IAccountRepo _accountRepo;

        public TransactionService(ITransactionRepo transactionRepo, IAccountRepo accountRepo)
        {
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
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
            var fromAccount = await _accountRepo.GetAccountByIdAsync(fromAccountId);
            var toAccount = await _accountRepo.GetAccountByIdAsync(toAccountId);

            if (fromAccount == null || toAccount == null)
                throw new Exception("Account not found.");

            if (fromAccount.Balance < amount)
                throw new Exception("Insufficient balance.");

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            await _accountRepo.UpdateAccountAsync(fromAccount);
            await _accountRepo.UpdateAccountAsync(toAccount);

            await _transactionRepo.AddTransactionAsync(new Transaction
            {
                AccountId = fromAccountId,
                Date = DateTime.UtcNow.Date,
                Type = "Debit",
                Operation = "Transfer",
                Amount = amount,
                Balance = fromAccount.Balance
            });

            await _transactionRepo.AddTransactionAsync(new Transaction
            {
                AccountId = toAccountId,
                Date = DateTime.UtcNow.Date,
                Type = "Credit",
                Operation = "Transfer",
                Amount = amount,
                Balance = toAccount.Balance
            });
        }

        public async Task TransferToAnotherCustomerAsync(int fromAccountId, int toAccountNumber, decimal amount)
        {
            await TransferBetweenAccountsAsync(fromAccountId, toAccountNumber, amount);
        }
    }
}
