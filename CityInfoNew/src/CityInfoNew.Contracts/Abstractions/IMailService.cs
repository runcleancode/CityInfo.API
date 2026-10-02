namespace CityInfoNew.Contracts.Abstractions;

public interface IMailService
{
    Task SendAsync(
        string subject,
        string message,
        CancellationToken cancellationToken = default);
}