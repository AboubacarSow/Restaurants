using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;

public class CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated):IAuthorizationRequirement
{
    public int MaximumRestaurantsCreated { get; private set; }=minimumRestaurantsCreated;
}
