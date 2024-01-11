using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BootesConsulta.Features.Auth;

public class LoginRequest : IRequest<LoginResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginResponse : BaseResponse
{
    public string Token { get; set; }
    public DateTime Expiry { get; set; }
}

public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly ILoginRepository _loginRepository;
    private readonly IConfiguration _configuration;

    public LoginHandler(ILoginRepository loginRepository, IConfiguration configuration)
    {
        _loginRepository = loginRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        bool result;
        try
        {
            result = await _loginRepository.CheckCredentials(new()
            {
                Password = request.Password,
                Email = request.UserName,
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        if (result)
        {
            string jwtKey = _configuration.GetSection("Jwt:Key").Get<string>();

            SymmetricSecurityKey secretKey = new(Encoding.ASCII.GetBytes(jwtKey));
            SigningCredentials singingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenOptions = new(
                claims: new List<Claim>(), expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: singingCredentials);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new LoginResponse()
            {
                Error = false,
                Message = null,
                Token = tokenString,
                Expiry = tokenOptions.ValidTo
            };
        }
        else
        {
            return new()
            {
                Error = true,
                Message = "Bad Credentials",
            };
        }

    }
}
