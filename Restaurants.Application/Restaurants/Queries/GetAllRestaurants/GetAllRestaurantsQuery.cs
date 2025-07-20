using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<(IEnumerable<RestaurantDto>, MetaData)>
{
    public bool TrackChanges { get; set; }
    public string? SearchTerm {  get; set; }
    public string? SortBy {  get; set; }
    public SortDirection? SortDirection { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

}
