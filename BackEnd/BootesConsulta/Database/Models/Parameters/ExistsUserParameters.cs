using Dapper;

namespace BootesConsulta.Database.Models;

public class ExistsUserParameters
{
    public const string SpName = "Exists_User";
    public string Email { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@Email", Email);
        return dynamicParameters;
    }
}
