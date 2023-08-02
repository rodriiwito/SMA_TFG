using Dapper;

namespace BootesConsulta.Database.Models;

public class SelectObservatoriosParameters
{

    public const string SpName = "Select_Observatorios";
    public int? AlturaMinima { get; set; }
    public int? AlturaMaxima { get; set; }
    public string Nombre { get; set; }

    public DynamicParameters GetDynamicParameters()
    {
        DynamicParameters dynamicParameters = new();
        dynamicParameters.Add("@AlturaMinima", AlturaMinima);
        dynamicParameters.Add("@AlturaMaxima", AlturaMaxima);
        dynamicParameters.Add("@Nombre", Nombre);
        return dynamicParameters;
    }
}
