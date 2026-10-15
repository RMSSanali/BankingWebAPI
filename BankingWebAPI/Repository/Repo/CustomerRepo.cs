using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingWebAPI.Repository.Repo
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankDbContext _dbContext;

        public CustomerRepo(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            // FindAsync hits by PK and is efficient. AsNoTracking for read-only.
            var entity = await _dbContext.Customers.FindAsync(customerId);
            if (entity != null) _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            var normalized = email.Trim();
            return await _dbContext.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Emailaddress == normalized);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
            // Deterministic paging: always order before Skip/Take
            return await _dbContext.Customers
                .AsNoTracking()
                .OrderBy(c => c.CustomerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existing = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);
            if (existing == null) return;

            // Update fields allow to change
            existing.Givenname = customer.Givenname;
            existing.Surname = customer.Surname;
            existing.Emailaddress = customer.Emailaddress;
            existing.Streetaddress = customer.Streetaddress;
            existing.City = customer.City;
            existing.Country = customer.Country;
            // (Optional) if want these editable too:
            // existing.Zipcode = customer.Zipcode;
            // existing.CountryCode = customer.CountryCode;
            // existing.Telephonecountrycode = customer.Telephonecountrycode;
            // existing.Telephonenumber = customer.Telephonenumber;
            // existing.Birthday = customer.Birthday;

            _dbContext.Customers.Update(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (customer == null) return;

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
