using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Command.CreateDish;
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
        return CreatedAtAction(nameof(CreateDish), id,null);
    }
}
