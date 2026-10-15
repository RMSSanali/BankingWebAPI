using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using BankingWebAPI.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepo _loanRepo;

    public LoanService(ILoanRepo loanRepo)
    {
        _loanRepo = loanRepo;
    }

    public async Task AddLoanAsync(Loan loan)
    {
        await _loanRepo.AddLoanAsync(loan);
    }
}
