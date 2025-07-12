using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
ILogger<CreateRestaurantCommandHandler> _logger, IMapper _mapper) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding a new restaurant item: {@Restaurant}",request);
        var restaurant = _mapper.Map<Restaurant>(request);
        var id = await restaurantsRepository.CreateAsync(restaurant);
        return id;
    }
}
