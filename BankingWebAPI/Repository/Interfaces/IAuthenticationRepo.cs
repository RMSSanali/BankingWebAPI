using BankingWebAPI.Repository.Entities;

namespace BankingWebAPI.Repository.Interfaces
{
    public interface IAuthenticationRepo
    {
        Task<User?> ValidateUser(string username, string password);
    }
}
