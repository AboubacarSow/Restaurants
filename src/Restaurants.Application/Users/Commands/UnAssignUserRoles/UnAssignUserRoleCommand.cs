using MediatR;

namespace Restaurants.Application.Users.Commands.UnAssignUserRoles;

public class UnAssignUserRoleCommand:IRequest
{
    public string UserEmail {  get; set; }
    public string RoleName {  get; set; }
}
