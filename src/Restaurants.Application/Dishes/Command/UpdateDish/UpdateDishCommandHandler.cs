using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Command.UpdateDish;

public class UpdateDishCommandHandler(IDishRepository _dishRepository,IRestaurantsRepository _restaurantsRepository,
    ILogger<UpdateDishCommandHandler> _logger,IMapper _mapper) : IRequestHandler<UpdateDishCommand>
{
    public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Editing Dish item: {@Dish}", request);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.RestaurantId, false)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dish = _mapper.Map<Dish>(request);
        await _dishRepository.UpdateDishAsync(dish);
    }
}
