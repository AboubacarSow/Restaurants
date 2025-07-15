using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[ApiController]// What does do this decoration attribute?
[Route("api/restaurants")]
public class RestaurantsController(IMediator _mediator) : ControllerBase
{

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(IEnumerable<RestaurantDto>))]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _mediator.Send(new GetAllRestaurantsQuery(false));
        return Ok(restaurants);

    }
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(RestaurantDto))]
    public async Task<IActionResult> Get([FromRoute]int id)
    {
        var restaurant=await _mediator.Send(new GetRestaurantByIdQuery(id,false));
        return Ok(restaurant);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateRestaurant( CreateRestaurantCommand model)
    {
        var id=await _mediator.Send(model);
        return CreatedAtAction(nameof(Get), new {id},null);
    }
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EditRestaurant(int id,UpdateRestaurantCommand model)
    {
        if (id != model.Id)
            return BadRequest(new { Message = "The given Id and the model id do not matched" });
         await _mediator.Send(model);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
       await _mediator.Send(new DeleteRestaurantCommand(id));
        return NoContent();
    }
}
