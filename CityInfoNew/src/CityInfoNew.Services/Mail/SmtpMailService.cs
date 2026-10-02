using CityInfoNew.Contracts.Abstractions;
using CityInfoNew.Entities.ConfigurationModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CityInfoNew.Services.Mail;

public class SmtpMailService : IMailService
{
    private readonly MailConfiguration _mailConfig;
    private readonly ILogger<SmtpMailService> _logger;

    public SmtpMailService(
        IOptions<MailConfiguration> mailConfig,
        ILogger<SmtpMailService> logger)
    {
        _mailConfig = mailConfig.Value;
        _logger = logger;
    }

    public async Task SendAsync(
        string subject,
        string message,
        CancellationToken cancellationToken = default)
    {
        var email = new MimeMessage();

        email.From.Add(MailboxAddress.Parse(_mailConfig.MailFromAddress));

        email.To.Add(MailboxAddress.Parse(_mailConfig.MailToAddress));

        email.Subject = subject;

        email.Body = new TextPart("plain")
        {
            Text = message
        };

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(
            _mailConfig.SmtpHost,
            _mailConfig.SmtpPort,
            _mailConfig.EnableSsl,
            cancellationToken);

        await smtpClient.SendAsync(email, cancellationToken);

        await smtpClient.DisconnectAsync(true, cancellationToken);

        _logger.LogInformation(
            "SMTP mail sent from {From} to {To} | Subject: {Subject}",
            _mailConfig.MailFromAddress,
            _mailConfig.MailToAddress,
            subject);

    }
}