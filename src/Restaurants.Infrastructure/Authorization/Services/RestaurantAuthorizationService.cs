using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

internal class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> _logger,IUserContext _userContext) 
    :IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = _userContext.GetCurrentUser();
        _logger.LogInformation("Authorizing {UserEmail},to {Operation } Restaurant {RestaurantName}",
            user?.Email,
            resourceOperation,
            restaurant.Name);

        if(resourceOperation==ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            _logger.LogInformation("{Operation} operation - successful Authorization",resourceOperation);
            return true;
        }
        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            _logger.LogInformation("Admin user - {Operation} operation sucessful Authorization", resourceOperation);
            return true;
        }
        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update) &&
            user?.Id == restaurant.OwnerId)
        {
            _logger.LogInformation("Owner user - {Operation} operation successfully Authorize", resourceOperation);
            return true;
        }
        return false;
    }
}
