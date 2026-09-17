namespace CityInfoNew.Entities.Models;

public class PointOfInterest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CityId { get; set; }

    //Ref:navigation property
    public City? City { get; init; }
}