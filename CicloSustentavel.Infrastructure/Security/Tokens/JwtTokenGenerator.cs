using CicloSustentavel.Domain.Models;
using CicloSustentavel.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CicloSustentavel.Infrastructure.Security.Tokens
{
    public class JwtTokenGenerator : IAccesTokenGenerator
    {
        private readonly uint _expirationTimeMinutes;
        private readonly string _secretKey;

        public JwtTokenGenerator(uint expirationTimeMinutes, string secretKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _secretKey = secretKey;
        }
        public string GenerateToken(UserModel user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey)),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
