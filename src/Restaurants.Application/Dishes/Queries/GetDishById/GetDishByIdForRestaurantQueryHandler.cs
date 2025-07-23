using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishById;

public class GetDishByIdForRestaurantQueryHandler(IDishRepository _dishRepository, IRestaurantsRepository _restaurantsRepository,
    ILogger<GetDishByIdForRestaurantQueryHandler> _logger, IMapper _mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retreiving Dish item with Id:{@DishId}", request.Id);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.RestaurantId, false)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dish = await _dishRepository.GetDishForRestaurant(request.Id, request.RestaurantId, false)
             ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());
        return _mapper.Map<DishDto>(dish);

    }
}

