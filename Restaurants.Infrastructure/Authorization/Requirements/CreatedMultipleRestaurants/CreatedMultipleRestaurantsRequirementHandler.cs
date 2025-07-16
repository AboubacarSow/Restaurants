using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;

public class CreatedMultipleRestaurantsRequirementHandler(IRestaurantsRepository _restaurantsRepository,IUserContext _userContext)
    : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
    {
        var user = _userContext.GetCurrentUser();

        var restaurants = await _restaurantsRepository.GetAllAsync(false);

        var restaurantCreated = restaurants.Count(r => r.OwnerId == user!.Id);
        if(restaurantCreated >= requirement.MaximumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

    }
}
