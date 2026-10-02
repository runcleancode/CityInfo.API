using System.ComponentModel.DataAnnotations;

namespace CityInfoNew.Entities.ConfigurationModels;

public record MailConfiguration
{
    [Required]
    public required string MailToAddress { get; init; }

    [Required]
    public required string MailFromAddress { get; init; }

    [Required]
    public required string SmtpHost { get; init; }

    [Range(1, 65535)]
    public required int SmtpPort { get; init; }

    public bool EnableSsl { get; init; }

}