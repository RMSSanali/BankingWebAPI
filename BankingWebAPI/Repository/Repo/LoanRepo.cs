using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using System.Threading.Tasks;

namespace BankingWebAPI.Repository.Repo
{
    public class LoanRepo : ILoanRepo
    {
        private readonly BankDbContext _dbContext;

        public LoanRepo(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddLoanAsync(Loan loan)
        {
            await _dbContext.Loans.AddAsync(loan);
            await _dbContext.SaveChangesAsync();
        }
    }
}
