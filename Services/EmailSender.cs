using Microsoft.AspNetCore.Identity.UI.Services;

namespace ProblemSolvingPlatform.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // For now, this is a stub implementation
        // In production, you would integrate with an email service like SendGrid or SMTP
        _logger.LogInformation($"Email to {email} - Subject: {subject}");
        _logger.LogInformation($"Message: {htmlMessage}");
        
        return Task.CompletedTask;
    }
}
