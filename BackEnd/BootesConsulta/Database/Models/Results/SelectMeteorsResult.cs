namespace BootesConsulta.Database.Models;

public class SelectMeteorsResult
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int TwoStationsReports { get; set; }
    public int OneStationReports { get; set; }
    public int PhotometryReports { get; set; }
}