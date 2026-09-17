
namespace CityInfoNew.Contracts.Contracts;

public interface IMailService
{
    void Send(string subject, string message);
}