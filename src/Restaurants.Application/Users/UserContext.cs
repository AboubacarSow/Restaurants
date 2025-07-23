using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Users;

public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = (_httpContextAccessor?.HttpContext?.User)
            ?? throw new InvalidOperationException("User context is not present");
        if (user.Identity == null || !user.Identity.IsAuthenticated)
            return null;
        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var user_email = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        var user_roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        var nationality=user.FindFirst(c=>c.Type=="Nationality")?.Value;
        var dateofBirthString = user.FindFirst(c => c.Type =="DateOfBirth")?.Value;
        var dateofBirth = dateofBirthString != null ? DateOnly.ParseExact(dateofBirthString, "yyyy-MM-dd") : (DateOnly?)null;
        return new CurrentUser(userId!, user_email!, user_roles,nationality!,dateofBirth);
    }
}