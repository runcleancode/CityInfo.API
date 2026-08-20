namespace CityInfo.Entities.Models;

public class PointOfInterest : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CityId { get; set; }
    public City? City { get; set; }
}
