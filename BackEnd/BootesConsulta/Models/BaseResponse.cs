namespace BootesConsulta.Models;

public class BaseResponse
{
    public bool Error { get; set; }
    public int MessageId { get; set; }
    public string Message { get; set; }
}
