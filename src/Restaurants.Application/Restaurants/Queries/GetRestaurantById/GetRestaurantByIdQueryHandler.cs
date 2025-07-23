using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(IRestaurantsRepository restaurantsRepository,IRestaurantAuthorizationService _restaurantAuthService,
ILogger<GetRestaurantByIdQueryHandler> _logger, IMapper _mapper,IUserContext _userContext) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        var user=_userContext.GetCurrentUser();
        _logger.LogInformation("{UserEmail} - Gettting Restaurant: {@Restaurant}",user.Email,request);
        var restaurant = await restaurantsRepository.GetOneRestaurantByIdAsync(id:request.Id, trackChanges:request.TrackChanges)
        ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        if(!_restaurantAuthService.Authorize(restaurant,ResourceOperation.Read))
            throw new ForbidenException(user.Email,ResourceOperation.Read.ToString());  
        return _mapper.Map<RestaurantDto>(restaurant);
    }
}
