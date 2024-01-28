using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Helpers;
using BootesConsulta.Models;
using BootesConsulta.Services;
using MediatR;

namespace BootesConsulta.Features.Auth;

public class LoginRequest : IRequest<LoginResponse>
{
    public string Email { get; set; }
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
    private readonly IEmailService _emailService;

    public LoginHandler(ILoginRepository loginRepository, IConfiguration configuration, IEmailService emailService)
    {
        _loginRepository = loginRepository;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        bool result;
        bool? emailConfirmed;
        try
        {
            emailConfirmed = await _loginRepository.ExistsUser(new()
            {
                Email = request.Email,
            });
            if (!emailConfirmed.HasValue)
            {
                return new()
                {
                    Error = true,
                    Message = "Credentials dont match."
                };
            }

            result = await _loginRepository.CheckCredentials(new()
            {
                Password = request.Password,
                Email = request.Email,
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        if (result)
        {
            (string, DateTime) tokenInfo = TokenHelper.GenerateToken(request.Email, _configuration);

            if (emailConfirmed.HasValue && !emailConfirmed.Value)
            {
                _emailService.Send(request.Email, tokenInfo.Item1);
                return new()
                {
                    Error = true,
                    Message = "Email sent for email validation."
                };
            }
            return new LoginResponse()
            {
                Error = false,
                Message = null,
                Token = tokenInfo.Item1,
                Expiry = tokenInfo.Item2
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
