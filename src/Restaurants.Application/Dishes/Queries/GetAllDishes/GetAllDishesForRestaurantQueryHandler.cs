using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

public class GetAllDishesForRestaurantQueryHandler(IDishRepository _dishRepository, IRestaurantsRepository _restaurantsRepository,
    ILogger<GetAllDishesForRestaurantQueryHandler> _logger,IMapper _mapper) : IRequestHandler<GetAllDishesForRestaurantQuery, List<DishDto>>
{
    public async Task<List<DishDto>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving all Dishes for For Restaurant with Id:{@RestaurantId}", request.RestaurantId);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.RestaurantId, false)
            ?? throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());
        var dishes = await _dishRepository.GetAllDishesForRestaurant(request.RestaurantId, false);
        return _mapper.Map<List<DishDto>>(dishes);  
    }
}
