using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
   Task<int> CreateDishAsync(Dish dish);
    Task UpdateDishAsync(Dish dish);
}
