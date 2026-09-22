using CityInfoNew.Contracts.Abstractions;
using CityInfoNew.Entities.ConfigurationModels;
using Microsoft.Extensions.Options;

namespace CityInfoNew.Services.Mail;

public class LocalMailService : IMailService
{
    private readonly MailConfiguration _mailConfig;

    public LocalMailService(IOptions<MailConfiguration> mailConfig)
    {
        _mailConfig = mailConfig.Value;
    }

    public void Send(string subject, string message)
    {
        Console.WriteLine($"Mail from {_mailConfig.MailFromAddress} to {_mailConfig.MailToAddress}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}