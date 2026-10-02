using CityInfoNew.Contracts.DTOs;

namespace CityInfoNew.Contracts.Abstractions;

public interface IAuthenticationService
{
    Task<string?> AuthenticateAsync(AuthenticationRequestBodyDto requestBody);
}