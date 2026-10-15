using BankingWebAPI.Repository.DTO;
using BankingWebAPI.Repository.Entities;
using BankingWebAPI.Services;
using BankingWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BankingWebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly ILoanService _loanService;

        public AdminController(
            ICustomerService customerService,
            IAccountService accountService,
            ILoanService loanService)
        {
            _customerService = customerService;
            _accountService = accountService;
            _loanService = loanService;
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO dto)
        {
            // Basic required checks to avoid NULL violations
            if (string.IsNullOrWhiteSpace(dto.Givenname) ||
                string.IsNullOrWhiteSpace(dto.Surname) ||
                string.IsNullOrWhiteSpace(dto.Streetaddress) ||
                string.IsNullOrWhiteSpace(dto.City) ||
                string.IsNullOrWhiteSpace(dto.Country))
            {
                return BadRequest("givenname, surname, streetaddress, city and country are required.");
            }

            //// Normalize email a bit
            //var email = dto.Emailaddress?.Trim();

            //// 1) Prevent duplicates by email (friendly 409)
            //if (!string.IsNullOrEmpty(email))
            //{
            //    var existing = await _customerService.GetCustomerByEmailAsync(email);
            //    if (existing != null)
            //        return Conflict($"Customer with email '{email}' already exists (CustomerId={existing.CustomerId}).");
            //}

            // Create the customer (fill all NOT NULL cols from your DB)
            var customer = new Customer
            {
                Gender = "other",                    // DB CHECK: male/female/other
                Givenname = dto.Givenname,
                Surname = dto.Surname,
                Streetaddress = dto.Streetaddress,
                City = dto.City,
                Zipcode = "00000",                   // default; optional to expose in DTO later
                Country = dto.Country,
                CountryCode = "SE",                  // default; optional to expose in DTO later
                Birthday = new DateTime(1995, 1, 1), // non-null DateTime in your entity
                Telephonecountrycode = null,
                Telephonenumber = null,
                //Emailaddress = email
            };

            await _customerService.AddCustomerAsync(customer);

            // Create default account (OWNER disposition)
            await _accountService.CreateDefaultAccountAsync(customer.CustomerId);

            return Ok(new { message = "Customer created", customerId = customer.CustomerId });
        }





        [HttpPost("AddLoan")]
        public async Task<IActionResult> AddLoan([FromBody] LoanDTO loanDTO)
        {
            // Add loan logic
            return Ok();
        }
    }
}
