using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Helpers;
using BootesConsulta.Models;
using BootesConsulta.Services;
using MediatR;

namespace BootesConsulta.Features.Auth;

public class RegisterRequest : IRequest<RegisterResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Country { get; set; }
    public string Organization { get; set; }
    public UserType UserType { get; set; }
}

public class RegisterResponse : BaseResponse
{
}

public class CreateUserHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly ILoginRepository _loginRepository;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public CreateUserHandler(ILoginRepository loginRepository, IConfiguration configuration, IEmailService emailService)
    {
        _loginRepository = loginRepository;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            bool? emailConfirmed = await _loginRepository.ExistsUser(new()
            {
                Email = request.Email,
            });

            if (emailConfirmed.HasValue && emailConfirmed.Value)
            {
                return new RegisterResponse()
                {
                    Error = true,
                    MessageId = 1,
                };
            }
            else if (emailConfirmed.HasValue && !emailConfirmed.Value)
            {
                (string, DateTime) tokenInfo = TokenHelper.GenerateToken(request.Email, _configuration);
                _emailService.Send(request.Email, tokenInfo.Item1);
                return new RegisterResponse()
                {
                    Error = true,
                    MessageId = 2,
                };
            }
            if (!emailConfirmed.HasValue)
            {
                await _loginRepository.CreateUser(new()
                {
                    Password = request.Password,
                    Email = request.Email,
                    Country = request.Country,
                    Organization = request.Organization,
                    UserType = request.UserType
                });
                (string, DateTime) tokenInfo = TokenHelper.GenerateToken(request.Email, _configuration);
                _emailService.Send(request.Email, tokenInfo.Item1);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return new()
        {
            Error = false
        };

    }
}
