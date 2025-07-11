using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers;

[ApiController]// What does do this decoration attribute?
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantsService _restauransService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants=await _restauransService.GetAllAsync(false);
        return Ok(restaurants);

    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var restaurant=await _restauransService.GetByIdAsync(id,false);
        return restaurant?.Id is null
            ? NotFound()
            : Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto model)
    {
        var id=await _restauransService.CreateRestaurantAsync(model);
        return CreatedAtAction(nameof(Get), new {id},null);
    }
}
