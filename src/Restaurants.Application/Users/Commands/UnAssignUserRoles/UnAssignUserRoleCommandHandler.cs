using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnAssignUserRoles;

public class UnAssignUserRoleCommandHandler(ILogger<UnAssignUserRoleCommandHandler> _logger,
    UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager) : IRequestHandler<UnAssignUserRoleCommand>
{
    public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Unassigning user role : {@Request}", request);
        var user=await _userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);
        var role=await _roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);
        var hasRole= await _userManager.IsInRoleAsync(user,role.Name!);
        if(hasRole)
        {
            var result=await _userManager.RemoveFromRoleAsync(user,role.Name!);      
        }
    }
}
