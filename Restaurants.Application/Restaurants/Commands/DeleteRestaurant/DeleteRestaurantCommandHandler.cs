using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository _restaurantsRepository,
    ILogger<DeleteRestaurantCommandHandler> _logger) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting Restaurant with Id: {RestaurantId}",request.Id);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.Id, false)
                    ?? throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        await _restaurantsRepository.DeleteAsync(restaurant);
    }
}
