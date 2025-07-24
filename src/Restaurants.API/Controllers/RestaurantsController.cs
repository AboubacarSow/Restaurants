using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using System.Text.Json;
using System.Threading.Tasks;

namespace Restaurants.API.Controllers;

[ApiController]// What does do this decoration attribute?
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController(IMediator _mediator) : ControllerBase
{

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type =typeof(IEnumerable<RestaurantDto>))]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantsQuery query)
    {
        var (restaurants,metaData)= await _mediator.Send(query);
        Response.Headers["X-Pagination"]=JsonSerializer.Serialize(metaData);
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
    [Authorize(Roles =UserRoles.Owner)]
    public async Task<IActionResult> CreateRestaurant( CreateRestaurantCommand model)
    {
        var id=await _mediator.Send(model);
        return CreatedAtAction(nameof(Get), new {id},null);
    }
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles =UserRoles.Admin)]
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
    [Authorize(Roles =UserRoles.Admin)]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
       await _mediator.Send(new DeleteRestaurantCommand(id));
        return NoContent();
    }


    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadRestaurantLogo([FromRoute]int id,IFormFile file)
    {
        using var stream= file.OpenReadStream();

        var command = new UploadRestaurantLogoCommand()
        {
            RestaurantId=id,
            FileName=$"{id}-{file.FileName}", 
            File=stream
        };
        await _mediator.Send(command);
        return NoContent();
    }
}
