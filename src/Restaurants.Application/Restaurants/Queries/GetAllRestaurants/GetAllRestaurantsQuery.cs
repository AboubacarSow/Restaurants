using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<(IEnumerable<RestaurantDto>, MetaData)>
{
    public bool trackChanges { get; set; } = false;
    public string? searchTerm {  get; set; }
    public string? sortBy {  get; set; }
    public SortDirection? sortDirection { get; set; }
    public int pageSize { get; set; }
    public int pageNumber { get; set; }

}
