namespace BootesConsulta.Database.Models;

public class TypeDistributionMeteorsResult
{
    public int OnlyOneStation { get; set; }
    public int OnlyTwoStation { get; set; }
    public int OnlyPhotometry { get; set; }
    public int OneAndTwoStation { get; set; }
    public int OneStationPhotometry { get; set; }
    public int TwoStationPhotometry { get; set; }
    public int AllTypes { get; set; }
    public int TotalNumber { get; set; }
}
