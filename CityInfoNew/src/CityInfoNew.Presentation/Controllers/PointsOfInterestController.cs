using Asp.Versioning;
using CityInfoNew.Contracts.Abstractions;
using CityInfoNew.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoNew.Presentation.Controllers;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    private readonly IServiceManager _manager;

    public PointsOfInterestController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPointsOfInterest()
    {
        var points = await _manager.PointOfInterestService.GetAllPointsOfInterestAsync(trackChanges: false);
        return Ok(points);
    }

    [HttpGet("{id:int}", Name = "GetOnePointOfInterestById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOnePointOfInterestByIdAsync(int id)
    {
        var point = await _manager.PointOfInterestService.GetOnePointOfInterestByIdAsync(id, trackChanges: false);

        return Ok(point);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOnePointOfInterestAsync([FromQuery] int cityId, PointOfInterestForCreationDto pointOfInterestDto)
    {
        var createdPoint = await _manager.PointOfInterestService.CreateOnePointOfInterest(cityId, pointOfInterestDto);

        return CreatedAtRoute(
            "GetOnePointOfInterestById",
            new { id = createdPoint.Id },
            createdPoint);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOnePointOfInterestAsync(
        [FromQuery] int cityId,
        [FromRoute(Name = "id")] int pointOfInterestId)
    {
        await _manager.PointOfInterestService.DeleteOnePointOfInterestAsync(cityId, pointOfInterestId, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOnePointOfInterestAsync(
        [FromQuery] int cityId,
        [FromRoute(Name = "id")] int pointOfInterestId,
        [FromBody] PointOfInterestForUpdateDto pointOfInterestDto)
    {
        if (pointOfInterestDto is null)
            return BadRequest("PointOfInterestForUpdateDto object is null");

        await _manager.PointOfInterestService.UpdateOnePointOfInterestAsync(cityId, pointOfInterestId, pointOfInterestDto, trackChanges: true);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PartiallyUpdatePointOfInterestAsync(
        [FromQuery] int cityId,
        [FromRoute(Name = "id")] int pointOfInterestId,
        [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    {
        if (patchDocument is null)
            return BadRequest("patchDocument object is null");

        var pointOfInterestToPatch = await _manager
            .PointOfInterestService
            .GetPointOfInterestForPatchAsync(cityId, pointOfInterestId, trackChanges: true);

        patchDocument.ApplyTo(
            pointOfInterestToPatch,
            error => ModelState.AddModelError(
                error.Operation.path,
                error.ErrorMessage));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _manager.PointOfInterestService
            .SaveChangesForPatch(
                cityId,
                pointOfInterestId,
                pointOfInterestToPatch,
                trackChanges: true);

        return NoContent();
    }
}