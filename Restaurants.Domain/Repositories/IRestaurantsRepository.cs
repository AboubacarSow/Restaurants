using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;
public interface IRestaurantsRepository
{
    Task<int> CreateAsync(Restaurant restaurant);
    Task DeleteAsync(Restaurant restaurant);
    Task<IEnumerable<Restaurant>> GetAllAsync(bool trackChanges);
    Task<Restaurant?> GetOneRestaurantByIdAsync(int id, bool trackChanges);

    Task SaveChangesAsync();
    //Task<Restaurant?> GetByIdAsync(Guid id);
    //Task AddAsync(Restaurant restaurant);
    //Task UpdateAsync(Restaurant restaurant);
    //Task DeleteAsync(Guid id);
}