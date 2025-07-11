using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos;

public  class DishAutoMapper : Profile
{
    public DishAutoMapper()
    {
        CreateMap<Dish, DishDto>();
    }
}
