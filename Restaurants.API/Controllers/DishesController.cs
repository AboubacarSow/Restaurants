using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Restaurants.Application.Dishes.Command.CreateDish;
using Restaurants.Application.Dishes.Command.DeleteDish;
using Restaurants.Application.Dishes.Queries.GetAllDishes;
using Restaurants.Application.Dishes.Queries.GetDishById;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers;


[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
public class DishesController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var id=await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDishByIdForRestaurant), new { restaurantId,dishId= id });
    }
    [HttpDelete("delete_one/{id:int}")]
    public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] int restaurantId, int id)
    {
        await _mediator.Send(new DeleteDishCommand(id,restaurantId));
        return NoContent();
    }

    [HttpDelete("delete_all")]
    public async Task<IActionResult> DeleteAllDishesForRestaurant([FromRoute]int restaurantId)
    {
        await _mediator.Send(new DeleteAllDishesForRestaurantCommand(restaurantId));
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDishesForRestaurant(int restaurantId)
    {
        var dishes = await _mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId:int}")]
    public async Task<IActionResult> GetDishByIdForRestaurant(int restaurantId,int dishId)
    {
        var dish=await _mediator.Send(new GetDishByIdForRestaurantQuery(dishId,restaurantId));
        return Ok(dish);
    }

}
