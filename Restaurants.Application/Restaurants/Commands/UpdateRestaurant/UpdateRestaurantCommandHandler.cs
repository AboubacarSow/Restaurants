using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IRestaurantsRepository _restaurantsRepository,IRestaurantAuthorizationService _restaurantAuthServie,
    ILogger<UpdateRestaurantCommandHandler> _logger, IUserContext _userContext) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user=_userContext.GetCurrentUser();
        _logger.LogInformation("User :{UserEmail} is editing restaurant with Id: {RestaurantId} and new one: {@NewRestaurant}",user!.Email,request.Id,request);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.Id,true)
                ?? throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        restaurant.Name = request.Name; 
        restaurant.Description = request.Description;
        restaurant.HasDelivery= request.HasDelivery;    
        restaurant.Category= request.Category;
        if(!_restaurantAuthServie.Authorize(restaurant,ResourceOperation.Update))
            throw new ForbidenException(user.Email, ResourceOperation.Update.ToString());
        await _restaurantsRepository.SaveChangesAsync();
    }
}
