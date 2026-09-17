using Asp.Versioning;
using CityInfoNew.Contracts.Contracts;
using CityInfoNew.Contracts.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoNew.Presentation.Controllers;

[ApiController]
[ApiVersionNeutral]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _service;

    public AuthenticationController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost("authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Authenticate(AuthenticationRequestBodyDto requestBody)
    {
        var token = await _service.AuthenticationService.AuthenticateAsync(requestBody);

        if (token is null)
            return Unauthorized();

        return Ok(token);
    }
}