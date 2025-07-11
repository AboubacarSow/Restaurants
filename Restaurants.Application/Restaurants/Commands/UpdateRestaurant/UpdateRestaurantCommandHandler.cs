using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IRestaurantsRepository _restaurantsRepository,
    ILogger<UpdateRestaurantCommandHandler> _logger, IMapper _mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Editing restaurant with Id: {request.Id}");
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.Id,true);
        if(restaurant == null ) 
            return false;
        restaurant.Name = request.Name; 
        restaurant.Description = request.Description;
        restaurant.HasDelivery= request.HasDelivery;    
        restaurant.Category= request.Category;
        await _restaurantsRepository.SaveChangesAsync();
        return true;
    }
}
