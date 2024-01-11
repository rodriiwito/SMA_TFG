using Dapper;

namespace BootesConsulta.Database.Models;

public class CreateUserParameters
{
    public const string SpName = "Register_User";
    public string Email { get; set; }
    public string Password { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@Email", Email);
        dynamicParameters.Add("@Password", Password);
        return dynamicParameters;
    }
}
