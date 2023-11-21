using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BootesConsulta.Features.Meteors;

[ApiController]
[Route("api/meteors")]
public class MeteorsController : Controller
{
    private readonly IMediator _mediator;
    public MeteorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("")]
    [SwaggerOperation("Meteors")]
    [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(MeteorsResponse))]
    public async Task<IActionResult> GetMeteors([FromBody] MeteorsRequest request)
    {
        MeteorsResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("distribution")]
    [SwaggerOperation("type distribution")]
    [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(TypeDistributionResponse))]
    public async Task<IActionResult> GetTypeDistribution()
    {
        TypeDistributionResponse response = await _mediator.Send(new TypeDistributionRequest());
        return Ok(response);
    }
}