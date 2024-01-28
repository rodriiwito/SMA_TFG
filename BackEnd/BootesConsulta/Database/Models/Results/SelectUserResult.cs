using BootesConsulta.Models;

namespace BootesConsulta.Database.Models;

public class SelectUserResult
{
    public string Email { get; set; }
    public string Country { get; set; }
    public string Organization { get; set; }
    public UserType UserType { get; set; }
}
