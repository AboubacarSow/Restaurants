using AutoMapper;
using Restaurants.Domain.Entities;


namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantAutoMapper :Profile
{
    public RestaurantAutoMapper()
    {
        CreateMap<CreateRestaurantDto, Restaurant>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode,
                }
            ));
    }
}
