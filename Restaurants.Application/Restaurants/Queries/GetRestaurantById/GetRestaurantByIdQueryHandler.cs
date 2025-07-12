using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(IRestaurantsRepository restaurantsRepository,
ILogger<GetRestaurantByIdQueryHandler> _logger, IMapper _mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Gettting Restaurant: {@Restaurant}",request);
        var restaurant = await restaurantsRepository.GetOneRestaurantByIdAsync(id:request.Id, trackChanges:request.TrackChanges);
        return _mapper.Map<RestaurantDto>(restaurant);
    }
}
