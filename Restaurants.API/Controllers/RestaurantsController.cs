using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers;

[ApiController]// What does do this decoration attribute?
[Route("api/restaurants")]
public class RestaurantsController(IMediator _mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _mediator.Send(new GetAllRestaurantsQuery(false));
        return Ok(restaurants);

    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var restaurant=await _mediator.Send(new GetRestaurantByIdQuery(id,false));
        return restaurant?.Id is null
            ? NotFound()
            : Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant( CreateRestaurantCommand model)
    {
        var id=await _mediator.Send(model);
        return CreatedAtAction(nameof(Get), new {id},null);
    }
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> EditRestaurant(int id,UpdateRestaurantCommand model)
    {
        var isEdited= await _mediator.Send(model);
        return isEdited ? NoContent() : NotFound($"Restaurant item with Id:{id} could not found");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var result = await _mediator.Send(new DeleteRestaurantCommand(id));
        return result? NoContent() : NotFound($"Restaurant with Id:{id} could not found");
    }
}
