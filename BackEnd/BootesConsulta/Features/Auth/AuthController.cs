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
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
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
}
