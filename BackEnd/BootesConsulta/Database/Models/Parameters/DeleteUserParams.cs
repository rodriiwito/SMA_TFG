using Dapper;

namespace BootesConsulta.Database.Models;

public class DeleteUserParams
{
    public const string SpName = "delete_user";
    public string Email { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@Email", Email);
        return dynamicParameters;
    }
}
