using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> _logger,
    IUserContext _userContext, IUserStore<User> _userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();
        _logger.LogInformation("Updating user: {@UserId} with {@Request}", user?.Id, request);

        var dbUser = await _userStore.FindByIdAsync(user!.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(User), user.Id.ToString());
        dbUser.DateOfBirth=request.DateOfBirth!.Value;
        dbUser.Nationaly=request.Nationality;
        await _userStore.UpdateAsync(dbUser,cancellationToken);
        
    }
}
