using Microsoft.AspNetCore.Identity;

namespace CityInfoNew.Entities.Models;

public class User : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public int? CityId { get; set; }

    //Ref:navigation property
    public City? City { get; init; }

}