using BankingWebAPI.Repository.DTO;
using BankingWebAPI.Repository.Entities;   // ⬅️ add this

namespace BankingWebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync(int pageNumber, int pageSize);
        Task<CustomerDTO?> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);

        // ⬅️ NEW: expose email lookup so controllers can check duplicates
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}
