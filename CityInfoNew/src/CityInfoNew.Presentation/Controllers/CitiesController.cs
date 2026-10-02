using System.Text.Json;
using Asp.Versioning;
using CityInfoNew.Contracts.Abstractions;
using CityInfoNew.Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoNew.Presentation.Controllers;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/cities")]
public class CitiesController : ControllerBase
{
    private readonly IServiceManager _manager;

    public CitiesController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCities([FromQuery] CityParameters cityParameters)
    {
        var (cities, metaData) = await _manager.CityService.GetAllCitiesAsync(cityParameters, trackChanges: false);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));
        Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");

        return Ok(cities);
    }

    /// <summary>
    /// Get a city by id
    /// </summary>
    /// <param name="id">The id of the city to get</param>
    /// <param name="includePointsOfInterest">Whether or not to include the points of interest</param>
    /// <returns>An IActionResult</returns>
    /// <response code ="200">Returns the requested city </response>
    [HttpGet("{id:int}", Name = "GetCityById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
    {
        var city = await _manager.CityService.GetOneCityByIdAsync(
            id,
            includePointsOfInterest,
            trackChanges: false);

        return Ok(city);
    }

    //For Testing
    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("Test exception for global exception handler.");
    }

    //For Testing
    [HttpGet("test-mail")]
    public async Task<IActionResult> TestMail()
    {
        await _manager.MailService.SendAsync(
            "CityInfo test mail",
            "This is a test email sent from CityInfoNew.");

        return Ok("Test mail sent.");
    }
}