using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BootesConsulta.Features.Observatories;

[ApiController]
[Route("api/observatories")]
public class ObservatoriesController : Controller
{
    private readonly IMediator _mediator;
    public ObservatoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("")]
    [SwaggerOperation("Observatorios")]
    [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ObservatoriosResponse))]
    public async Task<IActionResult> GetObservatorios([FromBody] ObservatoriosRequest request)
    {
        ObservatoriosResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}