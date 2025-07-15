using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Command.DeleteDish;

public class DeleteAllDishesForRestaurantCommandHandler(ILogger<DeleteAllDishesForRestaurantCommandHandler> _logger,
    IDishRepository _dishRepository,IRestaurantsRepository _restaurantsRepository) : IRequestHandler<DeleteAllDishesForRestaurantCommand>
{
    public async Task Handle(DeleteAllDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting all Dishes for Restaurant wiht Id:{@RestaurantId}", request.RestaurantId);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.RestaurantId, false)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        await _dishRepository.DeleteAllDishesAsync(request.RestaurantId);
    }
}
