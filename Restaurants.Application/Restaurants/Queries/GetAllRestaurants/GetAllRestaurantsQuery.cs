using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery(bool trackChanges): IRequest<IEnumerable<RestaurantDto>>
{
    public bool TrackChanges { get; set; } = trackChanges;
}
