using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BankingWebAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// New: generates a JWT with optional CustomerId claim.
        /// </summary>
        public string GenerateJwtToken(string username, string role, int? customerId = null)
        {
            try
            {
                var keyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var key = new SymmetricSecurityKey(keyBytes);
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                };

                if (customerId.HasValue)
                {
                    // Custom claim so CustomerController can read it: User.FindFirst("CustomerId")
                    claims.Add(new Claim("CustomerId", customerId.Value.ToString()));
                }

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating token: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Back-compat overload: generates a JWT without CustomerId (calls the new method).
        /// </summary>
        public string GenerateJwtToken(string username, string role)
            => GenerateJwtToken(username, role, customerId: null);
    }
}
