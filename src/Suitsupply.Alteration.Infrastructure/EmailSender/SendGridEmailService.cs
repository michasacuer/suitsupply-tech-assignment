using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Suitsupply.Alteration.Infrastructure.EmailSender;

public class SendGridEmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly IConfiguration _configuration;

    public SendGridEmailService(ISendGridClient sendGridClient, IConfiguration configuration)
    {
        _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task SendEmailAsync(string email, string body, string subject)
    {
        var msg = new SendGridMessage
        {
            PlainTextContent = body
        };
        
        msg.SetFrom(_configuration["SendGrid:From"], _configuration["SendGrid:FromName"]);
        msg.SetSubject(subject);
        msg.AddTo(email);
        
        await _sendGridClient.SendEmailAsync(msg);
    }
}