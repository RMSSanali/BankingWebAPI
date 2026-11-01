using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankingWebAPI.Repository.Repo
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly BankDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthenticationRepo(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> ValidateUser(string username, string password)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user is null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded
                ? user
                : null;
        }

        public Task<bool> UsernameExists(string username)
            => _dbContext.Users.AnyAsync(u => u.Username == username);

        public async Task<User> CreateUser(string username, string password, string role, int? customerId = null)
        {
            var user = new User
            {
                Username = username.Trim(),
                Password = _passwordHasher.HashPassword(new User(), password),
                Role = role,
                CustomerId = customerId
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
