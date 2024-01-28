using BootesConsulta.Database.Models;

namespace BootesConsulta.Database.Repository.Meteors;

public interface ILoginRepository
{
    Task CreateUser(CreateUserParameters parameters);
    Task<bool?> ExistsUser(ExistsUserParameters parameters);
    Task<bool> CheckCredentials(CheckCredentialsParameters parameters);
    Task VerifyUser(VerifyUserParams parameters);
    Task Delete(DeleteUserParams parameters);
    Task UpdateUser(UpdateUserParams parameters);
    Task<SelectUserResult> SelectUser(SelectUserParameters parameters);

}
