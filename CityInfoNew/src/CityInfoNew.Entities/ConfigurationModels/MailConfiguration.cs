using System.ComponentModel.DataAnnotations;

namespace CityInfoNew.Entities.ConfigurationModels;

public record MailConfiguration
{
    [Required]
    public required string MailToAddress { get; init; }
    [Required]
    public required string MailFromAddress { get; init; }
    public bool UseCloudMail { get; init; }
}