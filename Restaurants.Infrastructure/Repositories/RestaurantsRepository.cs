using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext _context) : IRestaurantsRepository
{
    public async Task<int> CreateAsync(Restaurant restaurant)
    {
        _context.Add(restaurant);
        await _context.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task DeleteAsync(Restaurant restaurant)
    {
        _context.Remove(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync(bool trackChanges)
    {
        return !trackChanges
                ?await _context.Restaurants
                            .AsNoTracking()
                            .Include(r => r.Dishes)
                            .ToListAsync()
                :await _context.Restaurants
                            .Include(r => r.Dishes)
                            .ToListAsync();
    }

    public async Task<Restaurant?> GetOneRestaurantByIdAsync(int id, bool trackChanges)
    {
        var restaurant = !trackChanges
            ? await _context.Restaurants.Where(r=>r.Id==id).AsNoTracking().Include(d=>d.Dishes).FirstOrDefaultAsync()
            : await _context.Restaurants.Where(r=>r.Id== id).Include(d => d.Dishes).FirstOrDefaultAsync();
        return restaurant;
    }

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

    // Uncomment and implement other methods as needed
    // public async Task<Restaurant?> GetByIdAsync(Guid id) { ... }
    // public async Task AddAsync(Restaurant restaurant) { ... }
    // public async Task UpdateAsync(Restaurant restaurant) { ... }
    // public async Task DeleteAsync(Guid id) { ... }
}