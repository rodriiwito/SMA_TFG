using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BootesConsulta.Features;

[ApiController]
[Route("api/consulta")]
public class ConsultaController : Controller
{
    private readonly IMediator _mediator;
    public ConsultaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("observatorios")]
    [SwaggerOperation("Observatorios")]
    [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ObservatoriosResponse))]
    public async Task<IActionResult> GetObservatorios([FromBody] ObservatoriosRequest request)
    {
        ObservatoriosResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}