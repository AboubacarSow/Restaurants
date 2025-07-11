using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Entities;


namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantAutoMapper :Profile
{
    public RestaurantAutoMapper()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode,
                }
            ));

        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(
                src =>(src.Address== null) ? null: src.Address.City ))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(
                src => (src.Address == null) ? null : src.Address.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(
                src => (src.Address == null) ? null : src.Address.PostalCode));
    }
}
