namespace BootesConsulta.Services;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

public interface IEmailService
{
    void Send(string to, string token);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Send(string to, string token)
    {
        string uri = _configuration["FrontUrl"] + "/verify-email/" + token;
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["Email:Email"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = "Validation email for SMA";
        email.Body = new TextPart(TextFormat.Html)
        {
            Text =
            "<p>Click this link, to finish the registration to the SMA.</p>" +
            "<a href=\"" + uri + "\">Go to SMA</a>" +
            "<p>In case you didn't register ignore this mail</p>"
        };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_configuration["Email:Host"], 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_configuration["Email:Email"], _configuration["Email:Password"]);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}