using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Xunit;

namespace Restaurants.Infrastructure.Tests.Authorization.Requirements.CreatedMultipleRestaurants;

public class CreatedMultipleRestaurantsRequirementHandlerTests
{
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
    private readonly Mock<IUserContext> _userContextMock;

    private readonly CreatedMultipleRestaurantsRequirementHandler _handler;

    public CreatedMultipleRestaurantsRequirementHandlerTests()
    {
        _restaurantsRepositoryMock= new Mock<IRestaurantsRepository>();
        _userContextMock = new Mock<IUserContext>();

        _handler = new CreatedMultipleRestaurantsRequirementHandler(
            _restaurantsRepositoryMock.Object,
            _userContextMock.Object
            );
    }
    [Fact()]
    public async Task HandleRequirementAsyncTest_UserHasCreatedMultipleRestaurant_ShouldSuccedAsync()
    {
        //Arrange
        //Retrieving currentUser
        var currentUser = new CurrentUser("12", "test@test.com", [], "German", null);
        _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        //Restaurants Items

        List<Restaurant> restaurans = [
            new (){
                OwnerId=currentUser.Id
            },
            new (){
                OwnerId=currentUser.Id
            } ,
            new (){
                OwnerId="12"
            }
            ];
        _restaurantsRepositoryMock.Setup(r=>r.GetAllAsync(false))
            .ReturnsAsync(restaurans);

        //var count= restaurans.Count();
        var requirement = new CreatedMultipleRestaurantsRequirement(2);
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, "12"),
            new (ClaimTypes.Email, "test@test.com"),
        };
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        var context = new AuthorizationHandlerContext([requirement],user,null);
        //Act

        await _handler.HandleAsync(context);

        //Assert

        context.HasSucceeded.Should().BeTrue();
    }
    [Fact()]
    public async Task HandleRequirementAsyncTest_UserHasCreatedMultipleRestaurant_ShouldFailAsync()
    {
        //Arrange
        //Retrieving currentUser
        var currentUser = new CurrentUser("12", "test@test.com", [], "German", null);
        _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        //Restaurants Items

        List<Restaurant> restaurans = [
            new (){
                OwnerId=currentUser.Id
            },
            new (){
                OwnerId="other"
            } ,
            new (){
                OwnerId="13"
            }
            ];
        _restaurantsRepositoryMock.Setup(r => r.GetAllAsync(false))
            .ReturnsAsync(restaurans);

        //var count= restaurans.Count();
        var requirement = new CreatedMultipleRestaurantsRequirement(2);
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, "12"),
            new (ClaimTypes.Email, "test@test.com"),
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        var context = new AuthorizationHandlerContext([requirement], user, null);
        //Act

        await _handler.HandleAsync(context);

        //Assert

        context.HasFailed.Should().BeTrue();
    }
}