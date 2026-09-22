using System.Text;
using CityInfoNew.Contracts.Abstractions;
using CityInfoNew.Contracts.DTOs;
using CityInfoNew.Entities.ConfigurationModels;
using CityInfoNew.Entities.Constants;
using CityInfoNew.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CityInfoNew.Services.Managers;

public class AuthenticationManager : IAuthenticationService
{
    private readonly IRepositoryManager _manager;
    private readonly JwtConfiguration _jwtConfiguration;

    public AuthenticationManager(
        IRepositoryManager manager,
        IOptions<JwtConfiguration> jwtConfiguration)
    {
        _manager = manager;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async Task<string?> AuthenticateAsync(AuthenticationRequestBodyDto requestBody)
    {
        var user = await _manager.User.GetUserByUserNameAsync(
                requestBody.UserName, trackChanges: false);

        if (user is null || user.PasswordHash is null)
            return null;

        var hasher = new PasswordHasher<User>();
        var verificationResult = hasher.VerifyHashedPassword(
                user, user.PasswordHash, requestBody.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
            return null;

        if (string.IsNullOrWhiteSpace(_jwtConfiguration.SecretForKey))
            throw new InvalidOperationException("Authentication:SecretForKey is not configured.");

        var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_jwtConfiguration.SecretForKey));

        var signinCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new Dictionary<string, object>
        {
            [JwtRegisteredClaimNames.Sub] = user.Id,
            [JwtRegisteredClaimNames.GivenName] = user.FirstName,
            [JwtRegisteredClaimNames.FamilyName] = user.LastName,
            [ClaimConstants.City] = user.City,

            ["SecurityStamp"] = user.SecurityStamp ?? string.Empty
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtConfiguration.Issuer,
            Audience = _jwtConfiguration.Audience,
            Claims = claims,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = signinCredentials
        };

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(tokenDescriptor);
    }
}