using BootesConsulta.Database.Repository.Meteors;
using BootesConsulta.Database.Repository.Observatories;
using BootesConsulta.Services;

namespace BootesConsulta.Extensions;

public class ServiceExtensions
{
    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IMeteorsRepository, MeteorsRepository>();
        services.AddScoped<IObservatoriesRepository, ObservatoriesRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
