namespace CityInfo.Entities.Models;

public class City : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();
}
