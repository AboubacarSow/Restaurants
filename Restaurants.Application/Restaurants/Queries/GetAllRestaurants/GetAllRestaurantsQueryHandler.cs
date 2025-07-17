using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities.RequestFeatures;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository,
ILogger<GetAllRestaurantsQueryHandler> _logger, IMapper _mapper) : IRequestHandler<GetAllRestaurantsQuery, (IEnumerable<RestaurantDto>,MetaData)>
{
    public async Task<(IEnumerable<RestaurantDto>, MetaData)> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting All Restaurants");
        var restaurantsWithPagedList = await restaurantsRepository
            .GetAllWithMatchingAsync(request.SearchTerm,
                                    request.PageSize,
                                    request.PageNumber,
                                    request.SortBy,
                                    request.SortDirection,
                                     request.TrackChanges);
        var restaurantDtos=_mapper.Map<IEnumerable<RestaurantDto>>(restaurantsWithPagedList);
        var metaData=restaurantsWithPagedList.MetaData;
        return (restaurantDtos,metaData);
    }
}
