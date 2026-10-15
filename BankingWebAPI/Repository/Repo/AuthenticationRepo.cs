using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingWebAPI.Repository.Repo
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly BankDbContext _dbContext;

        public AuthenticationRepo(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> ValidateUser(string username, string password)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
