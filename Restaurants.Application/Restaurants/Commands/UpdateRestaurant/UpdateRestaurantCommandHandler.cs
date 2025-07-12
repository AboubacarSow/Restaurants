using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IRestaurantsRepository _restaurantsRepository,
    ILogger<UpdateRestaurantCommandHandler> _logger, IMapper _mapper) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Editing restaurant with Id: {RestaurantId} and new one: {@NewRestaurant}",request.Id,request);
        var restaurant = await _restaurantsRepository.GetOneRestaurantByIdAsync(request.Id,true)
                ?? throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        restaurant.Name = request.Name; 
        restaurant.Description = request.Description;
        restaurant.HasDelivery= request.HasDelivery;    
        restaurant.Category= request.Category;
        await _restaurantsRepository.SaveChangesAsync();
    }
}
