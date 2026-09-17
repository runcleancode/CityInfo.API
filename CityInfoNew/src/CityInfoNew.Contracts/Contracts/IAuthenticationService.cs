using CityInfoNew.Contracts.DTOs;

namespace CityInfoNew.Contracts.Contracts;

public interface IAuthenticationService
{
    Task<string?> AuthenticateAsync(AuthenticationRequestBodyDto requestBody);
}