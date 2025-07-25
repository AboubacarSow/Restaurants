using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Seeders;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.API.Tests.Controllers;

public class RestaurantsControllerTests :IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();
    private readonly Mock<IUserContext> _userContextMock = new();
    private readonly Mock<IRestaurantConfig> _restaurantSeederMock= new();  
    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        var currentUser = new CurrentUser("12", "test@test.com", ["Admin"], "German", null);
        _userContextMock.Setup(u=>u.GetCurrentUser()).Returns(currentUser);
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                    _ => _restaurantsRepositoryMock.Object));

                services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantConfig),
                    _ => _restaurantSeederMock.Object));


                services.AddSingleton(implementationInstance: _userContextMock.Object);

            });
        });

    }

    [Fact()]
    public async Task GetAll_ForValidRequest_ShouldReturns200Ok()
    {
      //Arrange
      var client= _factory.CreateClient();
        //Act

        var result = await client.GetAsync("/api/restaurants?trackChanges=false&sortDirection=0&pageSize=10&pageNumber=5");

        //Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    [Fact()]
    public async Task GetAll_ForInvalidRequest_ShouldReturns400BadRequest()
    {
        //Arrange
        var client = _factory.CreateClient();
        //Act
        var result = await client.GetAsync("/api/restaurants");
        

        //Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);   
    }


    [Fact()]
    public async Task Get_ForNonExistingId_ShouldReturns404NotFound()
    {
        //Arrange
        var restaurantId = 12345930;
        _restaurantsRepositoryMock.Setup(r => r.GetOneRestaurantByIdAsync(restaurantId, false))
            .ReturnsAsync((Restaurant?)null);

        var client = _factory.CreateClient();

        //Act
        var result = await client.GetAsync($"/api/restaurants/{restaurantId}");

        //Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
      
    }
    [Fact()]
    public async Task Get_ForExistingId_ShouldRetursn200Ok()
    {
        //Arrange
        var restaurantId = 999270;
        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test description"
        };
        _restaurantsRepositoryMock.Setup(r => r.GetOneRestaurantByIdAsync(restaurantId, false))
            .ReturnsAsync(restaurant);
        var client = _factory.CreateClient();
        //Act
        var result = await client.GetAsync($"/api/restaurants/{restaurantId}");
        var restaurantDto=await result.Content.ReadFromJsonAsync<RestaurantDto>();
        //Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurantId);
        restaurant.Name.Should().Be("Test");

      
    }

    
}