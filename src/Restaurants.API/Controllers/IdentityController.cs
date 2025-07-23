using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRoles;
using Restaurants.Application.Users.Commands.UnAssignUserRoles;
using Restaurants.Application.Users.Commands.UpdateUserDetails;
using Restaurants.Domain.Constants;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator _mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    [HttpPost("userRole")]
    [Authorize(Roles =UserRoles.Admin)] 
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles =UserRoles.Admin)]
    public async Task<IActionResult> UnAssignUserRoleAsync(UnAssignUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
