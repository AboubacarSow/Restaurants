using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;

public class MinimumAgeRequirementHandler(IUserContext _userContext,ILogger<MinimumAgeRequirementHandler> _logger)
    : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var currentUser = _userContext.GetCurrentUser();

        _logger.LogInformation("User email {@Email}, date of Birth: {@DOB} Handling MinimumAge Requirement",
            currentUser?.Email,
            currentUser?.DateOfBirth);

        if(currentUser?.DateOfBirth==null)
        {
            _logger.LogWarning("User Date of Birth is not define");
            context.Fail();
            return Task.CompletedTask;
        }
        if(currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge)<= DateOnly.FromDateTime(DateTime.Today))
        {
            _logger.LogInformation("Authorization Succeeded");
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        else
        {
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
