using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Stokify.Features.Users.Models;

namespace Stokify.Services;

public static class JwtToken
{
    public static string Key { get; set; } = null!;

    extension(User user)
    {
        public static string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyBytes = Encoding.UTF8.GetBytes(Key);

            var credenctial = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credenctial,
                Expires = DateTime.UtcNow.AddHours(4),
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}