using Restaurants.Domain.Entities;
using Restaurants.Domain.Entities.RequestFeatures;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Extensions.Restaurants;

public static class RestaurantsExtensions
{
    public static IQueryable<Restaurant> SearchRestaurants(this IQueryable<Restaurant> query, string? searchTerm)
    {
        if(searchTerm == null)  
            return query;
        var lowerCaseTerm = searchTerm.ToLower();
        query = query
            .Where(r => r.Name.Equals(lowerCaseTerm, StringComparison.CurrentCultureIgnoreCase) ||
                    r.Description.Equals(lowerCaseTerm, StringComparison.CurrentCultureIgnoreCase));
        return query;
    }
    public static IQueryable<Restaurant> SortRestaurants(this IQueryable<Restaurant> query, string? sortBy, SortDirection? sortDirection)
    {
        if (sortBy == null)
            return query.OrderBy(r => r.Id);
        var columnSelector = new Dictionary<string, Expression<Func<Restaurant, Object>>>
        {
            {nameof(Restaurant.Name),r=>r.Name},
            {nameof(Restaurant.Description),r=>r.Description},
            {nameof(Restaurant.Category),r=>r.Category}
        };
        var selectedColumns = columnSelector[sortBy];

        return sortDirection== SortDirection.Ascending
            ? query.OrderBy(selectedColumns)
            : query.OrderByDescending(selectedColumns);
        
    }
}
