using System.ComponentModel.DataAnnotations;

namespace CityInfoNew.Contracts.DTOs;

// Patch/Update for → class (mutable)
public abstract class PointOfInterestForManipulation
{
    [Required(ErrorMessage = "You should provide a name value.")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }
}