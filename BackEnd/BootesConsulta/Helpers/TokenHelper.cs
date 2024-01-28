using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BootesConsulta.Helpers;

public static class TokenHelper
{
    public static (string, DateTime) GenerateToken(string email, IConfiguration _configuration)
    {
        string jwtKey = _configuration.GetSection("Jwt:Key").Get<string>();

        SymmetricSecurityKey secretKey = new(Encoding.ASCII.GetBytes(jwtKey));
        SigningCredentials singingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken tokenOptions = new(
            claims: new List<Claim> { (new Claim("email", email)) }, expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: singingCredentials);
        string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return (tokenString, tokenOptions.ValidTo);
    }

    public static string GetEmail(string token)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.Where(claim => claim.Type == "email").First().Value;
    }
}
