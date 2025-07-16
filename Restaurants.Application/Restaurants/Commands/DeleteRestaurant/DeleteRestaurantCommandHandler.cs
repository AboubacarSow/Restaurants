using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository _restaurantsRepository, IRestaurantAuthorizationService _restaurantAuthorizationService,
    ILogger<DeleteRestaurantCommandHandler> _logger, IUserContext _userContext) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();
        _logger.LogInformation("User {UserEmail} is deleting Restaurant with Id: {RestaurantId}",user?.Email,request.Id);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.Id, false)
                    ?? throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidenException(user!.Email, ResourceOperation.Create.ToString());
        await _restaurantsRepository.DeleteAsync(restaurant);
    }
}
