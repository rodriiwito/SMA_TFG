using Dapper;

namespace BootesConsulta.Database.Models;

public class TypeDistributionMeteorsParameters
{
    public const string SpName = "Type_Distribution";
    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        return dynamicParameters;
    }
}
