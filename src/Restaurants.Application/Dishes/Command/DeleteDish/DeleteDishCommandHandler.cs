using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Command.DeleteDish;

public class DeleteDishCommandHandler(ILogger<DeleteDishCommandHandler> _logger, IMapper _mapper,
    IDishRepository _dishRepository,IRestaurantsRepository _restaurantsRepository) : IRequestHandler<DeleteDishCommand>
{
    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting Dish with Id: {DishId}", request.Id);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.RestaurantId, false)
           ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dish = await _dishRepository.GetDishForRestaurant(request.Id,request.RestaurantId, false)
             ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());
        await _dishRepository.DeleteDishAsync(dish);
    }
}
