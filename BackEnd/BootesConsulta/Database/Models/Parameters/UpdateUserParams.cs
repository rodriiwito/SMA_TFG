using BootesConsulta.Models;
using Dapper;

namespace BootesConsulta.Database.Models;

public class UpdateUserParams
{
    public const string SpName = "update_user";
    public string Email { get; set; }
    public string Country { get; set; }
    public string Organization { get; set; }
    public UserType UserType { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@Email", Email);
        dynamicParameters.Add("@Country", Country);
        dynamicParameters.Add("@Organization", Organization);
        dynamicParameters.Add("@UserType", UserType);
        return dynamicParameters;
    }
}
