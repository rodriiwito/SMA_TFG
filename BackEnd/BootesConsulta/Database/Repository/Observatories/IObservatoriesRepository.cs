using BootesConsulta.Database.Models;

namespace BootesConsulta.Database.Repository.Observatories;

public interface IObservatoriesRepository
{
    Task<IEnumerable<SelectObservatoriosResult>> SelectObservatorios(SelectObservatoriosParameters parameters);

}
