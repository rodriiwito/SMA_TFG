using BootesConsulta.Database.Models;
using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Models;
using MediatR;

namespace BootesConsulta.Features.Meteors;

public class TypeDistributionRequest : IRequest<TypeDistributionResponse>
{
}

public class TypeDistributionResponse : BaseResponse
{
    public int OnlyOneStation { get; set; }
    public int OnlyTwoStation { get; set; }
    public int OnlyPhotometry { get; set; }
    public int OneAndTwoStation { get; set; }
    public int OneStationPhotometry { get; set; }
    public int TwoStationPhotometry { get; set; }
    public int AllTypes { get; set; }
    public int TotalNumber { get; set; }
}

public class TypeDistributionHandler : IRequestHandler<TypeDistributionRequest, TypeDistributionResponse>
{
    private readonly IMeteorsRepository _meteorsRepository;

    public TypeDistributionHandler(IMeteorsRepository meteorsRepository)
    {
        _meteorsRepository = meteorsRepository;
    }

    public async Task<TypeDistributionResponse> Handle(TypeDistributionRequest request, CancellationToken cancellationToken)
    {
        TypeDistributionMeteorsResult result;
        try
        {
            result = await _meteorsRepository.TypeDistributionMeteors(new TypeDistributionMeteorsParameters());
        }
        catch (Exception ex)
        {
            return new()
            {
                Error = true,
                Message = ex.ToString()
            };
        }

        return new()
        {
            Error = false,
            Message = null,
            AllTypes = result.AllTypes,
            OneAndTwoStation = result.OneAndTwoStation,
            OneStationPhotometry = result.OneStationPhotometry,
            OnlyOneStation = result.OnlyOneStation,
            OnlyPhotometry = result.OnlyPhotometry,
            OnlyTwoStation = result.OnlyTwoStation,
            TotalNumber = result.TotalNumber,
            TwoStationPhotometry = result.TwoStationPhotometry
        };
    }
}
