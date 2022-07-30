namespace Suitsupply.Alteration.Infrastructure.EmailSender;

public interface IEmailService
{
    Task SendEmailAsync(string email, string template);
}