using BootesConsulta.Database.Models;

namespace BootesConsulta.Database.Repository;

public interface IConsultaRepository
{
    Task<IEnumerable<SelectObservatoriosResult>> SelectObservatorios(SelectObservatoriosParameters parameters);

}
