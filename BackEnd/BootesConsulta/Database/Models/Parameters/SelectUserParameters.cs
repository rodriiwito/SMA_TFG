using Dapper;

namespace BootesConsulta.Database.Models;

public class SelectUserParameters
{
    public const string SpName = "Select_User";
    public string Email { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@Email", Email);
        return dynamicParameters;
    }
}
