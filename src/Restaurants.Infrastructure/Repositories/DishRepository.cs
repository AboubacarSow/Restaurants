using Microsoft.EntityFrameworkCore;
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
    public async Task DeleteDishAsync(Dish dish)
    {
        _context.Remove(dish);
        await _context.SaveChangesAsync();
    }
    public async Task<Dish?> GetDishForRestaurant(int id,int restaurantId,bool trackChanges)
    {
        var dish = !trackChanges
                   ? await _context.Dishes.Where(r => r.Id == id && r.RestaurantId==restaurantId).AsNoTracking().FirstOrDefaultAsync()
                   : await _context.Dishes.Where(r => r.Id == id && r.RestaurantId == restaurantId).FirstOrDefaultAsync();
        return dish;
    }
    public async Task<Dish?> GetOneDishByIdAsync(int id,bool trackChanges)
    {
        var dish = !trackChanges
                    ? await _context.Dishes.Where(r => r.Id == id).AsNoTracking().FirstOrDefaultAsync()
                    : await _context.Dishes.Where(r => r.Id == id).FirstOrDefaultAsync();
        return dish;
    }

    public async Task DeleteAllDishesAsync(int restaurantId)
    {
        var dishes= await _context.Dishes.Where(r=>r.RestaurantId==restaurantId).ToListAsync();
        _context.RemoveRange(dishes);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Dish>> GetAllDishesForRestaurant(int restaurantId, bool trackChanges)
    {
        var dishes = !trackChanges
                   ? await _context.Dishes.Where(r => r.RestaurantId == restaurantId).AsNoTracking().ToListAsync()
                   : await _context.Dishes.Where(r => r.RestaurantId == restaurantId).ToListAsync();
        return dishes;
    }
}
