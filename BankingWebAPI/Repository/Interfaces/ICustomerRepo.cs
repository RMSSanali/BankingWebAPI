using BankingWebAPI.Repository.Entities;

namespace BankingWebAPI.Repository.Interfaces
{
    public interface ICustomerRepo
    {
        Task AddCustomerAsync(Customer customer); // Correct - Only one definition
        Task<Customer?> GetCustomerByIdAsync(int customerId); // Correct - Only one definition
        Task<Customer?> GetCustomerByEmailAsync(string email);

        Task<IEnumerable<Customer>> GetAllCustomersAsync(int pageNumber, int pageSize); // For pagination
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
    }
}
