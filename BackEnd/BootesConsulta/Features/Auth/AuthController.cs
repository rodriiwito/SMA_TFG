using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootesConsulta.Features.Auth;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILoginRepository _loginRepository;
    public AuthController(IMediator mediator, ILoginRepository loginRepository)
    {
        _mediator = mediator;
        _loginRepository = loginRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        LoginResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterRequest request)
    {
        RegisterResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("verify-email/{token}")]
    public async Task<IActionResult> VerifyEmail([FromRoute] string token)
    {
        try
        {
            await _loginRepository.VerifyUser(new()
            {
                Email = TokenHelper.GetEmail(token),
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Ok();
    }
}
