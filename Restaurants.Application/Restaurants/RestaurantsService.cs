using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
ILogger<RestaurantsService> _logger, IMapper _mapper) : IRestaurantsService
{
    public async Task<int> CreateRestaurantAsync(CreateRestaurantDto model)
    {
        _logger.LogInformation("Adding a new restaurant item");
        var restaurant= _mapper.Map<Restaurant>(model);  
        var id=await restaurantsRepository.CreateAsync(restaurant);
        return id;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync(bool trackChanges)
    {
        _logger.LogInformation("Getting All Restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync(trackChanges);
        return restaurants.Select(RestaurantDto.FromEntity)!;

    }

    public async Task<RestaurantDto?> GetByIdAsync(int id,  bool trackChanges)
    {
        _logger.LogInformation($"Gettting Restaurant with Id: {id}");
        var restaurant = await restaurantsRepository.GetOneRestaurantByIdAsync(id, trackChanges);
        return  RestaurantDto.FromEntity(restaurant);
    }
}
