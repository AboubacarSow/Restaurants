using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
ILogger<CreateRestaurantCommandHandler> _logger, IMapper _mapper) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding a new restaurant item");
        var restaurant = _mapper.Map<Restaurant>(request);
        var id = await restaurantsRepository.CreateAsync(restaurant);
        return id;
    }
}
