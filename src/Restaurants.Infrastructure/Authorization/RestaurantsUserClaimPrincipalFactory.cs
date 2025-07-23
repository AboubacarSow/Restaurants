using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantsUserClaimPrincipalFactory(UserManager<User> _userManager,
    RoleManager<IdentityRole> _roleManager, IOptions<IdentityOptions> _options)
    : UserClaimsPrincipalFactory<User, IdentityRole>(_userManager,_roleManager,_options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id= await GenerateClaimsAsync(user);
        if (user.Nationaly != null)
            id.AddClaim(new Claim(AppClaimTypes.Nationality,user.Nationaly));
        if (user?.DateOfBirth != null)
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.ToString("yyyy-MM-dd")));
        return new ClaimsPrincipal(id);
    }
}
