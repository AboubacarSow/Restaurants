﻿using AutoMapper;
using Restaurants.Application.Dishes.Command.CreateDish;
using Restaurants.Application.Dishes.Command.UpdateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos;

public  class DishAutoMapper : Profile
{
    public DishAutoMapper()
    {
        CreateMap<Dish, DishDto>();
        CreateMap<CreateDishCommand, Dish>();
        CreateMap<UpdateDishCommand, Dish>().ReverseMap();
    }
}
