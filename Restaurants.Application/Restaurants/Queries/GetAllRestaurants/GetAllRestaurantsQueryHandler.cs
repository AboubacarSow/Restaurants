using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository,
ILogger<GetAllRestaurantsQueryHandler> _logger, IMapper _mapper) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting All Restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync(trackChanges: request.TrackChanges);
        return _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
    }
}
