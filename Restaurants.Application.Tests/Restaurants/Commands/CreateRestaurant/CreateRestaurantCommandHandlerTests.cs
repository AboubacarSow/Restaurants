using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Application.Tests.Users;
using Moq;
using Castle.Core.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Users;
using FluentAssertions;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<CreateRestaurantCommandHandler>> _loggerMock; 
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;   
    private readonly Mock<IUserContext> _userContextMock;

    private readonly CreateRestaurantCommandHandler _handler;

    public CreateRestaurantCommandHandlerTests()
    {
        _loggerMock= new Mock<ILogger<CreateRestaurantCommandHandler>>();
        _mapperMock= new Mock<IMapper>();
        _restaurantAuthorizationServiceMock=new Mock<IRestaurantAuthorizationService>();
        _restaurantsRepositoryMock= new Mock<IRestaurantsRepository>();
        _userContextMock= new Mock<IUserContext>();

        _handler = new CreateRestaurantCommandHandler(
            _restaurantsRepositoryMock.Object,
            _loggerMock.Object,
            _restaurantAuthorizationServiceMock.Object,
            _mapperMock.Object,
            _userContextMock.Object);
    }
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnCreatedRestaurantId()
    {
        //Arrange
        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();

        _mapperMock.Setup(m=>m.Map<Restaurant>(command)).Returns(restaurant);
        //Creating restaurantRepositoryMock
        _restaurantsRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var currentUser = new CurrentUser("owner-id", "test@gmail", ["Admin"], null,null);
        _userContextMock.Setup(u=>u.GetCurrentUser()).Returns(currentUser);


        _restaurantAuthorizationServiceMock.Setup(r => r.Authorize(restaurant, ResourceOperation.Create))
            .Returns(true);
        //Act
        var result = await _handler.Handle(command, CancellationToken.None);


        //Assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        _restaurantsRepositoryMock.Verify(r => r.CreateAsync(restaurant), Times.Once);

    }
}