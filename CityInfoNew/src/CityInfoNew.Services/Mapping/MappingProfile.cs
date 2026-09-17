using AutoMapper;
using CityInfoNew.Contracts.DTOs;
using CityInfoNew.Entities.Models;

namespace CityInfoNew.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityWithoutPointsOfInterestDto>();
        CreateMap<PointOfInterestForCreationDto, PointOfInterest>().ReverseMap();
        CreateMap<PointOfInterest, PointOfInterestDto>();
        CreateMap<City, CityDto>();
        CreateMap<PointOfInterestForUpdateDto, PointOfInterest>().ReverseMap();
    }
}