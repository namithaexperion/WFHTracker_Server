using MailKit.Net.Smtp;
using MimeKit;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendManagerApprovalEmail()
    {
        var message = new MimeMessage();

        message.From.Add(
            MailboxAddress.Parse(
                _configuration["EmailSettings:FromEmailAddress"]));

        message.To.Add(
            MailboxAddress.Parse("namitha.augustine@experionglobal.com"));

        message.Subject = "Manager Approval";

        message.Body = new TextPart("plain")
        {
            Text = $@"
                    Please find details of the wfh request in below link.


                    Regards,
                    Team HR
"
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            _configuration["EmailSettings:SmtpServer"],
            int.Parse(_configuration["EmailSettings:SmtpPort"]!),
            MailKit.Security.SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            _configuration["EmailSettings:SmtpUsername"],
            _configuration["EmailSettings:SmtpPassword"]);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }

    public async Task SendGMApprovalEmail()
    {
        var message = new MimeMessage();

        message.From.Add(
            MailboxAddress.Parse(
                _configuration["EmailSettings:FromEmailAddress"]));

        message.To.Add(
            MailboxAddress.Parse("namitha.augustine@experionglobal.com"));

        message.Subject = "GM Approval";

        message.Body = new TextPart("plain")
        {
            Text = $@"
                    Please find details of the wfh request in below link.


                    Regards,
                    Team HR
"
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            _configuration["EmailSettings:SmtpServer"],
            int.Parse(_configuration["EmailSettings:SmtpPort"]!),
            MailKit.Security.SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            _configuration["EmailSettings:SmtpUsername"],
            _configuration["EmailSettings:SmtpPassword"]);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }

    public async Task SendHRApprovalEmail()
    {
        var message = new MimeMessage();

        message.From.Add(
            MailboxAddress.Parse(
                _configuration["EmailSettings:FromEmailAddress"]));

        message.To.Add(
            MailboxAddress.Parse("alan.jose@experionglobal.com"));

        message.Subject = "Manager Approval";

        message.Body = new TextPart("plain")
        {
            Text = $@"
                    Hi Alan,

                    Please find details of the wfh request of your employee in below link.
                    http://localhost:5173/approvals/1


                    Regards,
                    Team HR
"
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            _configuration["EmailSettings:SmtpServer"],
            int.Parse(_configuration["EmailSettings:SmtpPort"]!),
            MailKit.Security.SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            _configuration["EmailSettings:SmtpUsername"],
            _configuration["EmailSettings:SmtpPassword"]);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }

    public async Task SendRejectioEmail()
    {
        var message = new MimeMessage();

        message.From.Add(
            MailboxAddress.Parse(
                _configuration["EmailSettings:FromEmailAddress"]));

        message.To.Add(
            MailboxAddress.Parse("alan.jose@experionglobal.com"));

        message.Subject = "WFH Approval";

        message.Body = new TextPart("plain")
        {
            Text = $@"
                    Hi Alan,

                    Your WFH request is Approved.


                    Regards,
                    Team HR
"
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            _configuration["EmailSettings:SmtpServer"],
            int.Parse(_configuration["EmailSettings:SmtpPort"]!),
            MailKit.Security.SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            _configuration["EmailSettings:SmtpUsername"],
            _configuration["EmailSettings:SmtpPassword"]);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }

    public async Task SendApprovalEmail()
    {
        var message = new MimeMessage();

        message.From.Add(
            MailboxAddress.Parse(
                _configuration["EmailSettings:FromEmailAddress"]));

        message.To.Add(
            MailboxAddress.Parse("namitha.augustine@experionglobal.com"));

        message.Subject = "WFH Approval";

        message.Body = new TextPart("plain")
        {
            Text = $@"
                    Your WFH request is Approved.


                    Regards,
                    Team HR
"
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            _configuration["EmailSettings:SmtpServer"],
            int.Parse(_configuration["EmailSettings:SmtpPort"]!),
            MailKit.Security.SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            _configuration["EmailSettings:SmtpUsername"],
            _configuration["EmailSettings:SmtpPassword"]);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }
}