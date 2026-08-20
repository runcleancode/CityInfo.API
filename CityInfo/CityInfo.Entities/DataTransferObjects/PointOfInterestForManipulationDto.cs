using System.ComponentModel.DataAnnotations;

namespace CityInfo.Entities.DataTransferObjects;

public abstract class PointOfInterestForManipulationDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "Description must not exceed 200 characters.")]
    public string? Description { get; set; }
}
