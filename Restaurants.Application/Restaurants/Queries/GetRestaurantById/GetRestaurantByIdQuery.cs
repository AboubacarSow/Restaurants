using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery(int id,bool trackChanges): IRequest<RestaurantDto?>
{
    public int Id { get; set; } = id;
    public bool TrackChanges { get; set; } = trackChanges;
}
