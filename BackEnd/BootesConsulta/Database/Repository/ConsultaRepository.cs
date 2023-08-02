using BootesConsulta.Database.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace BootesConsulta.Database.Repository;

public class ConsultaRepository : IConsultaRepository
{
    private readonly IConfiguration _configuration;

    public ConsultaRepository(IConfiguration configuration)
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
        return await connection.QueryAsync<SelectObservatoriosResult>(SelectObservatoriosParameters.SpName, parameters.GetDynamicParameters(), commandType: CommandType.StoredProcedure);
    }
}
