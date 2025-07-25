using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api")]
public class RootController : ControllerBase
{
    /// <summary>
    /// Root endpoint for the Restaurants API.
    /// </summary>
    [HttpGet]
    public IActionResult GetInfo()
    {
        var baseUrl = $"{Request.Scheme}//{Request.Host}";
        var aboutApi= new
        {
            name = "Restaurants API",
            version = "v1",
            description = "Welcome to the Restaurants API. Use this endpoint to explore available routes.",
            documentation = new
            {
                swaggerUI = Url.Content($"{baseUrl}/swagger/index.html"),
                swaggerJson = $"{baseUrl}/swagger/v1/swagger.json"
            }
        };

        return Ok(aboutApi);
    }
}
