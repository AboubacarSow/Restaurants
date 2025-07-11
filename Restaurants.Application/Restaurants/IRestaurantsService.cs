using Restaurants.Application.Restaurants.Dtos;
namespace Restaurants.Application.Restaurants;

public interface IRestaurantsService
{
    Task<int> CreateRestaurantAsync(CreateRestaurantDto model);
    Task<IEnumerable<RestaurantDto>> GetAllAsync(bool trackChanges);
    Task<RestaurantDto?> GetByIdAsync(int id, bool trackChanges);
}