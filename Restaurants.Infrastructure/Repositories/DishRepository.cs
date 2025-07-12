using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository(RestaurantsDbContext _context) : IDishRepository
{
    public async Task<int> CreateDishAsync(Dish dish)
    {
        _context.Add(dish);
        await _context.SaveChangesAsync();
        return dish.Id;
    }

    public async Task UpdateDishAsync(Dish dish)
    {
        _context.Update(dish);
        await _context.SaveChangesAsync();
    }
}
