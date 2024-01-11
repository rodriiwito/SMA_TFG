using BootesConsulta.Database.Models;

namespace BootesConsulta.Database.Repository.Meteors;

public interface ILoginRepository
{
    Task CreateUser(CreateUserParameters parameters);
    Task<bool> CheckCredentials(CheckCredentialsParameters parameters);
}
