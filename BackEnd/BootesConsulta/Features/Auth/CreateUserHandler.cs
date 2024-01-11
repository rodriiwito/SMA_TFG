using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Models;
using MediatR;

namespace BootesConsulta.Features.Auth;

public class RegisterRequest : IRequest<RegisterResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterResponse : BaseResponse
{
}

public class CreateUserHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly ILoginRepository _loginRepository;

    public CreateUserHandler(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _loginRepository.CreateUser(new()
            {
                Password = request.Password,
                Email = request.Email,
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return new()
        {
            Error = false,
            Message = null
        };

    }
}
