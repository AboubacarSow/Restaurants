using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
    Task<int> CreateDishAsync(Dish dish);
    Task DeleteAllDishesAsync(int restaurantId);
    Task DeleteDishAsync(Dish dish);
    Task<IEnumerable<Dish>> GetAllDishesForRestaurant(int restaurantId, bool trackChanges);
    Task<Dish?> GetDishForRestaurant(int id, int restaurantId, bool trackChanges);
    Task<Dish?> GetOneDishByIdAsync(int id,bool trackChanges);
    Task UpdateDishAsync(Dish dish);
}
