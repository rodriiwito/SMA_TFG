using BootesConsulta.Database.Models;
using BootesConsulta.Database.Repository.Observatories;
using BootesConsulta.Models;
using MediatR;

namespace BootesConsulta.Features.Observatories;

public class ObservatoriosRequest : IRequest<ObservatoriosResponse>
{
    public int? AlturaMaxima { get; set; }
    public int? AlturaMinima { get; set; }
    public string Nombre { get; set; }
}

public class ObservatoriosResponse : BaseResponse
{
    public IEnumerable<Observatorio> Observatories { get; set; }
}

public class Observatorio
{
    public string Numero { get; set; }
    public string Nombre { get; set; }
    public string Longitud { get; set; }
    public string Latitud { get; set; }
    public int Altura { get; set; }
    public string Creditos { get; set; }
}
public class ObservatoriosHandler : IRequestHandler<ObservatoriosRequest, ObservatoriosResponse>
{
    private readonly IObservatoriesRepository _observatoriesRepository;

    public ObservatoriosHandler(IObservatoriesRepository observatoriesRepository)
    {
        _observatoriesRepository = observatoriesRepository;
    }

    public async Task<ObservatoriosResponse> Handle(ObservatoriosRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<SelectObservatoriosResult> result = null;
        try
        {
            result = await _observatoriesRepository.SelectObservatorios(new()
            {
                AlturaMinima = request.AlturaMinima,
                AlturaMaxima = request.AlturaMaxima,
                Nombre = request.Nombre
            });
        }
        catch (Exception ex)
        {
            return new ObservatoriosResponse()
            {
                Error = true,
                Message = ex.ToString()
            };
        }

        return new ObservatoriosResponse()
        {
            Error = false,
            Message = null,
            Observatories = result.Select(observatorio => new Observatorio()
            {
                Altura = observatorio.Altitud,
                Creditos = observatorio.Créditos,
                Latitud = observatorio.Latitud_Sexagesimal,
                Longitud = observatorio.Longitud_Sexagesimal,
                Nombre = observatorio.Nombre_Observatorio,
                Numero = observatorio.Número
            })
        };
    }
}
