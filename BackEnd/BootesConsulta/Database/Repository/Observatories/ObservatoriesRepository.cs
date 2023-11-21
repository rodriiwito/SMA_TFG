using BootesConsulta.Database.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace BootesConsulta.Database.Repository.Observatories;

public class ObservatoriesRepository : IObservatoriesRepository
{
    private readonly IConfiguration _configuration;

    public ObservatoriesRepository(IConfiguration configuration)
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

    public async Task<IEnumerable<SelectObservatoriosResult>> SelectObservatorios(SelectObservatoriosParameters parameters)
    {
        using IDbConnection connection = Connection;
        try
        {
            return await connection.QueryAsync<SelectObservatoriosResult>(SelectObservatoriosParameters.SpName, parameters.GetDynamicParameters(), commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al ejecutar el procedimiento almacenado" + SelectObservatoriosParameters.SpName, ex);
        }
    }
}
