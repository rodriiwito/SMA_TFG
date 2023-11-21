using BootesConsulta.Models;
using Dapper;

namespace BootesConsulta.Database.Models;

public class SelectMeteorsParameters
{

    public const string SpName = "Select_Meteoros";
    public DateTime? MinimunDate { get; set; }
    public DateTime? MaximumDate { get; set; }
    public IEnumerable<ReportType> ReportTypes { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@MinimunDate", MinimunDate);
        dynamicParameters.Add("@MaximumDate", MaximumDate);
        dynamicParameters.Add("@ReportTypes", DataBaseHelper.EnumToDataTable(ReportTypes));
        return dynamicParameters;
    }
}
