using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository _restaurantsRepository,
    ILogger<DeleteRestaurantCommandHandler> _logger) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting Restaurant with Id: {RestaurantId}",request.Id);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.Id, false);
        if (restaurant == null)
            return false;
        await _restaurantsRepository.DeleteAsync(restaurant);
        return true;
    }
}
