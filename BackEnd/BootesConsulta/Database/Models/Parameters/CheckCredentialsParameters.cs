using Dapper;
using System.Data;

namespace BootesConsulta.Database.Models;

public class CheckCredentialsParameters
{
    public const string SpName = "Check_Credentials";
    public string Email { get; set; }
    public string Password { get; set; }

    public bool IsValid { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@Email", Email);
        dynamicParameters.Add("@Password", Password);

        dynamicParameters.Add("@IsValid", IsValid, DbType.Boolean, direction: ParameterDirection.Output);
        return dynamicParameters;
    }
}
