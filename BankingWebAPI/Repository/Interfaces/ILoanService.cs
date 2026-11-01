using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using BankingWebAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace BankingWebAPI.Services
{
    public interface ILoanService
    {
        Task<int> AddLoanAsync(int customerId, decimal amount, int duration);
    }
}
