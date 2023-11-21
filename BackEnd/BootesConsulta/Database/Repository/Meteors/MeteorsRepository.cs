using BootesConsulta.Database.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace BootesConsulta.Database.Repository.Meteors;

public class MeteorsRepository : IMeteorsRepository
{
    private readonly IConfiguration _configuration;

    public MeteorsRepository(IConfiguration configuration)
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

    public async Task<IEnumerable<SelectMeteorsResult>> SelectMeteors(SelectMeteorsParameters parameters)
    {
        using IDbConnection connection = Connection;

        try
        {
            return await connection.QueryAsync<SelectMeteorsResult>(SelectMeteorsParameters.SpName, parameters.GetDynamicParameters(), commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al ejecutar el procedimiento almacenado" + SelectObservatoriosParameters.SpName, ex);
        }
    }

    public async Task<TypeDistributionMeteorsResult> TypeDistributionMeteors(TypeDistributionMeteorsParameters parameters)
    {
        using IDbConnection connection = Connection;

        try
        {
            return await connection.QueryFirstAsync<TypeDistributionMeteorsResult>(TypeDistributionMeteorsParameters.SpName, parameters.GetDynamicParameters(), commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al ejecutar el procedimiento almacenado" + SelectObservatoriosParameters.SpName, ex);
        }
    }
}