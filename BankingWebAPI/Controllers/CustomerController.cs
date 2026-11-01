using BankingWebAPI.Services.Interfaces;
using BankingWebAPI.Repository.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingWebAPI.Controllers
{
    [Authorize(Roles = "Customer")] // use "Admin,Customer" while testing if needed
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public CustomerController(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        private int GetCustomerIdFromToken()
            => int.Parse(User.FindFirst("CustomerId")?.Value ?? "0");

        // 1) Get Accounts (no query string)
        [HttpGet("Accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var customerId = GetCustomerIdFromToken();
            if (customerId == 0) return Forbid("No CustomerId in token.");

            var accounts = await _accountService.GetAccountsByCustomerIdAsync(customerId);
            return Ok(accounts);
        }

        // 2) Get Transactions for an Account
        [HttpGet("Transactions/{accountId}")]
        public async Task<IActionResult> GetTransactions(int accountId)
        {
            var customerId = GetCustomerIdFromToken();
            if (customerId == 0 || !await _accountService.IsOwnedByCustomerAsync(accountId, customerId))
                return Forbid();

            var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId);
            return Ok(transactions);
        }

        // 3) Create a New Bank Account (no customerId query param)
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO accountDTO)
        {
            var customerId = GetCustomerIdFromToken();
            if (customerId == 0) return Forbid("No CustomerId in token.");

            await _accountService.CreateAccountAsync(customerId, accountDTO.AccountTypeId, accountDTO.InitialDeposit);
            return Ok(new { message = "Account created successfully." });
        }

        // 4) Transfer Between Accounts
        [HttpPost("TransferBetweenAccounts")]
        public async Task<IActionResult> TransferBetweenAccounts([FromBody] TransferDTO transferDTO)
        {
            var customerId = GetCustomerIdFromToken();
            if (customerId == 0 ||
                !await _accountService.IsOwnedByCustomerAsync(transferDTO.FromAccountId, customerId) ||
                !await _accountService.IsOwnedByCustomerAsync(transferDTO.ToAccountId, customerId))
                return Forbid();

            try
            {
                await _transactionService.TransferBetweenAccountsAsync(
                    transferDTO.FromAccountId, transferDTO.ToAccountId, transferDTO.Amount);
                return Ok(new { message = "Transfer successful." });
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        }

        // 5) Transfer to Another Customer
        [HttpPost("TransferToCustomer")]
        public async Task<IActionResult> TransferToCustomer([FromBody] TransferToCustomerDTO transferDTO)
        {
            var customerId = GetCustomerIdFromToken();
            if (customerId == 0 ||
                !await _accountService.IsOwnedByCustomerAsync(transferDTO.FromAccountId, customerId))
                return Forbid();

            try
            {
                await _transactionService.TransferToAnotherCustomerAsync(
                    transferDTO.FromAccountId, transferDTO.ToAccountNumber, transferDTO.Amount);
                return Ok(new { message = "Transfer to customer successful." });
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
        }
    }
}
