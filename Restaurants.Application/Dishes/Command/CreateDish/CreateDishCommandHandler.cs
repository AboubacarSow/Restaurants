using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Command.CreateDish;

public class CreateDishCommandHandler(IDishRepository _dishRepository,IRestaurantsRepository _restaurantsRepository,IMapper _mapper,
    ILogger<CreateDishCommandHandler> _logger) : IRequestHandler<CreateDishCommand,int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding a new Dish item: {@Dish}", request);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.RestaurantId, false)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dish =_mapper.Map<Dish>(request);
        var result= await _dishRepository.CreateDishAsync(dish);
        return result;  
    }
}
