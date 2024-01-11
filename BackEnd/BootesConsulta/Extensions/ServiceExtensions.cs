using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Database.Repository.Observatories;

namespace BootesConsulta.Extensions;

public class ServiceExtensions
{
    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IMeteorsRepository, MeteorsRepository>();
        services.AddScoped<IObservatoriesRepository, ObservatoriesRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();
    }
}
