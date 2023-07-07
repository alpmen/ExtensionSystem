using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        public string CreateToken()
        {
            var bytes = Encoding.UTF8.GetBytes("DemoProjectSecretKey");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(issuer: "https://localhost", audience: "https://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(10), signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
