using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;

        public PointsOfInterestController(CitiesDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore
                ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city is null)
                return NotFound();

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{id}")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int id)
        {
            var city = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city is null)
                return NotFound();

            var pointOfInterest = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfInterest is null)
                return NotFound($"Id : {id} was not found.");

            return Ok(pointOfInterest);
        }
    }
}