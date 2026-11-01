using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingWebAPI.Services;

public class LoanService : ILoanService
{
    private readonly BankDbContext _dbContext;

    public LoanService(BankDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddLoanAsync(int customerId, decimal amount, int duration)
    {
        if (amount <= 0) throw new ArgumentException("Loan amount must be greater than zero.");
        if (duration <= 0) throw new ArgumentException("Loan duration must be greater than zero.");

        var account = await _dbContext.Dispositions
            .Where(d => d.CustomerId == customerId && d.Type == "OWNER")
            .Select(d => d.Account)
            .FirstOrDefaultAsync();

        if (account is null)
            throw new InvalidOperationException("The customer does not have an owned account.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        account.Balance += amount;

        var loan = new Loan
        {
            AccountId = account.AccountId,
            Date = DateTime.UtcNow.Date,
            Amount = amount,
            Duration = duration,
            Payments = decimal.Round(amount / duration, 2),
            Status = "Active"
        };

        _dbContext.Loans.Add(loan);
        _dbContext.Transactions.Add(new Transaction
        {
            AccountId = account.AccountId,
            Date = DateTime.UtcNow.Date,
            Type = "Credit",
            Operation = "Loan",
            Amount = amount,
            Balance = account.Balance
        });

        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return loan.LoanId;
    }
}
