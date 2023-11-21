using BootesConsulta.Database.Models;

namespace BootesConsulta.Database.Repository.Meteors;

public interface IMeteorsRepository
{
    Task<IEnumerable<SelectMeteorsResult>> SelectMeteors(SelectMeteorsParameters parameters);
    Task<TypeDistributionMeteorsResult> TypeDistributionMeteors(TypeDistributionMeteorsParameters parameters);
}
