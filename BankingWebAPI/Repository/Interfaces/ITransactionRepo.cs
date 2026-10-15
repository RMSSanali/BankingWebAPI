using BankingWebAPI.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingWebAPI.Repository.Interfaces
{
    public interface ITransactionRepo
    {
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        Task AddTransactionAsync(Transaction transaction);
    }
}
