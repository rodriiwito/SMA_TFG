using BootesConsulta.Database.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace BootesConsulta.Database.Repository.Meteors;

public class LoginRepository : ILoginRepository
{
    private readonly IConfiguration _configuration;

    public LoginRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection Connection
    {
        get
        {
            return new SqlConnection(_configuration.GetConnectionString("BootesDB"));
        }
    }

    public async Task<bool> CheckCredentials(CheckCredentialsParameters parameters)
    {
        using IDbConnection connection = Connection;
        DynamicParameters dynamicparameters = parameters.GetDynamicParameters();
        try
        {
            await connection.QueryAsync(CheckCredentialsParameters.SpName, dynamicparameters, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al ejecutar el procedimiento almacenado" + CheckCredentialsParameters.SpName, ex);
        }
        return dynamicparameters.Get<bool>("@IsValid");
    }

    public async Task CreateUser(CreateUserParameters parameters)
    {
        using IDbConnection connection = Connection;

        try
        {
            await connection.ExecuteAsync(CreateUserParameters.SpName, parameters.GetDynamicParameters(), commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al ejecutar el procedimiento almacenado" + CreateUserParameters.SpName, ex);
        }
    }
}