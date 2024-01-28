using Dapper;

namespace BootesConsulta.Database.Models;

public class VerifyUserParams
{
    public const string SpName = "Verify_User";
    public string Email { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@Email", Email);
        return dynamicParameters;
    }
}
