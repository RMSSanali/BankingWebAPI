using BankingWebAPI.Repository.DTO;
using BankingWebAPI.Repository.Interfaces;
using BankingWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

public class AuthenticationController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly IAuthenticationRepo _authRepo;

    public AuthenticationController(TokenService tokenService, IAuthenticationRepo authRepo)
    {
        _tokenService = tokenService;
        _authRepo = authRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        var user = await _authRepo.ValidateUser(request.Username, request.Password);

        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = _tokenService.GenerateJwtToken(user.Username, user.Role, user.CustomerId);
        return Ok(new { Token = token });
    }
}
