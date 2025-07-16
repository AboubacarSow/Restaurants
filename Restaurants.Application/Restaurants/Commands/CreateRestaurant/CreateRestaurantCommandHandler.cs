using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
ILogger<CreateRestaurantCommandHandler> _logger,IRestaurantAuthorizationService _restaurantAuthorizationService,
IMapper _mapper,IUserContext _userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user=_userContext.GetCurrentUser();
        _logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Request}",
            user!.Email,
            user.Id,
            request);
        var restaurant = _mapper.Map<Restaurant>(request);
        restaurant.OwnerId=user!.Id;
        if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
            throw new ForbidenException(user.Email, ResourceOperation.Create.ToString());
        var id = await restaurantsRepository.CreateAsync(restaurant);
        return id;
    }
}
