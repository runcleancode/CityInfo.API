namespace CityInfoNew.Contracts.Abstractions;

public interface IMailService
{
    void Send(string subject, string message);
}