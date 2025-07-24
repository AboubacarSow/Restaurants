using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;

public class UploadRestaurantLogoCommandHandler(ILogger<UploadRestaurantLogoCommandHandler> _logger,
    IRestaurantsRepository _restaurantsRepository,IBlobStorageService _blobStorageService,
    IUserContext _userContext,IRestaurantAuthorizationService _restaurantAuthService) : IRequestHandler<UploadRestaurantLogoCommand>
{
    public async Task Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Uploading restaurant log for id: {RestaurantId}",request.RestaurantId);
        var user = _userContext.GetCurrentUser();
        _logger.LogInformation("User :{UserEmail} is editing restaurant with Id: {RestaurantId} and new one: {@NewRestaurant}", user!.Email, request.RestaurantId, request);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.RestaurantId, true)
                    ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        if (!_restaurantAuthService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidenException(user.Email, ResourceOperation.Update.ToString());


        restaurant.LogoUrl = await _blobStorageService.UploadToBlogAsync(request.File, request.FileName);
        await _restaurantsRepository.SaveChangesAsync();
    }
}
