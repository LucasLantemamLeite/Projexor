using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Projexor.Features.Users.Models;

namespace Projexor.Services;

public static class JwtToken
{
    public static string Key { get; set; } = null!;

    extension(User user)
    {
        public string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyBytes = Encoding.UTF8.GetBytes(Key);

            var credenctial = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credenctial,
                Expires = DateTime.UtcNow.AddHours(4),

                Subject = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())])
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}