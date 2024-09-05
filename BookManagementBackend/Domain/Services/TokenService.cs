using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookManagementBackend.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> _optionsAppSettings)
        {
            _appSettings = _optionsAppSettings.Value;
        }

        public string GenerateToken(Users user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] tokenKey = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);

            ClaimsIdentity claims = new([
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")                
            ]);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(9),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
