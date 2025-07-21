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

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnCreatedRestaurantId()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();

        //How to fake a mapping?
        mapperMock.Setup(m=>m.Map<Restaurant>(command)).Returns(restaurant);
        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@gmail", [], null,null);
        var restaurantAuthorizeService = new Mock<IRestaurantAuthorizationService>();
        userContextMock
            .Setup(u=>u.GetCurrentUser()).Returns(currentUser);
        var commandHandler= new CreateRestaurantCommandHandler
            (restaurantRepositoryMock.Object,loggerMock.Object,
            restaurantAuthorizeService.Object,mapperMock.Object,userContextMock.Object);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);


        //Assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantRepositoryMock.Verify(r => r.CreateAsync(restaurant), Times.Once);
       
    }
}