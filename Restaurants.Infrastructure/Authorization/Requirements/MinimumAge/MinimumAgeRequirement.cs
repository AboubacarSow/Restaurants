using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;

public  class MinimumAgeRequirement(int minimumAge) :IAuthorizationRequirement
{
    public int MinimumAge {  get; set; }=minimumAge;
}
