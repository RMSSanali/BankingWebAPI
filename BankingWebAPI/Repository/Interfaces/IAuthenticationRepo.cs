using BankingWebAPI.Repository.Entities;

namespace BankingWebAPI.Repository.Interfaces
{
    public interface IAuthenticationRepo
    {
        Task<User?> ValidateUser(string username, string password);
        Task<bool> UsernameExists(string username);
        Task<User> CreateUser(string username, string password, string role, int? customerId = null);
    }
}
