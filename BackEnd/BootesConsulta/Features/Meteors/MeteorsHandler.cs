using BootesConsulta.Database.Models;
using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Models;
using MediatR;

namespace BootesConsulta.Features.Meteors;

public class MeteorsRequest : IRequest<MeteorsResponse>
{
    public DateTime? MinimunDate { get; set; }
    public DateTime? MaximumDate { get; set; }
    public IEnumerable<ReportType> ReportTypes { get; set; }
}

public class MeteorsResponse : BaseResponse
{
    public IEnumerable<Meteor> Meteors { get; set; }
}

public class Meteor
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int TwoStationsReports { get; set; }
    public int OneStationReports { get; set; }
    public int PhotometryReports { get; set; }
}
public class MeteorsHandler : IRequestHandler<MeteorsRequest, MeteorsResponse>
{
    private readonly IMeteorsRepository _meteorsRepository;

    public MeteorsHandler(IMeteorsRepository meteorsRepository)
    {
        _meteorsRepository = meteorsRepository;
    }

    public async Task<MeteorsResponse> Handle(MeteorsRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<SelectMeteorsResult> result = null;
        try
        {
            result = await _meteorsRepository.SelectMeteors(new()
            {
                MaximumDate = request.MaximumDate,
                MinimunDate = request.MinimunDate,
                ReportTypes = request.ReportTypes
            });
        }
        catch (Exception ex)
        {
            return new MeteorsResponse()
            {
                Error = true,
                Message = ex.ToString()
            };
        }

        return new MeteorsResponse()
        {
            Error = false,
            Message = null,
            Meteors = result.Select(meteor => new Meteor()
            {
                Date = meteor.Fecha,
                Id = meteor.Id,
                OneStationReports = meteor.OneStationReports,
                PhotometryReports = meteor.PhotometryReports,
                TwoStationsReports = meteor.TwoStationsReports
            })
        };
    }
}
