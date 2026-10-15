using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Repository.Interfaces;
using BankingWebAPI.Repository.DTO;
using BankingWebAPI.Services.Interfaces;

namespace BankingWebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
            var customers = await _customerRepo.GetAllCustomersAsync(pageNumber, pageSize);

            return customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                Givenname = c.Givenname,
                Surname = c.Surname,
                Emailaddress = c.Emailaddress
            });
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(customerId);

            if (customer == null) return null;

            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Streetaddress = customer.Streetaddress,
                City = customer.City,
                Country = customer.Country,
                Emailaddress = customer.Emailaddress
            };
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepo.AddCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepo.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepo.DeleteCustomerAsync(customerId);
        }
        
        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _customerRepo.GetCustomerByEmailAsync(email);
        }
    }
}