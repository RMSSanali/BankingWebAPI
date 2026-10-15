using BankingWebAPI.Repository.Entities;
using System.Threading.Tasks;

namespace BankingWebAPI.Repository.Interfaces
{
    public interface ILoanRepo
    {
        Task AddLoanAsync(Loan loan); // Correctly typed Task and proper interface declaration
    }
}
