using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Repositories;
using AutoMapper;
using Restaurants.Domain.Interfaces;
using Restaurants.Application.Users;
using Xunit;
using Restaurants.Domain.Entities;
using FluentAssertions;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Tests.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandlerTests
{
    private  readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _logger;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IRestaurantAuthorizationService> _authorizationService;
    private readonly Mock<IUserContext> _userContext;

    private readonly UpdateRestaurantCommandHandler _handler;

    public UpdateRestaurantCommandHandlerTests()
    {
       _logger= new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _mapper= new Mock<IMapper>();
        _restaurantsRepository=new Mock<IRestaurantsRepository>();
        _authorizationService=new Mock<IRestaurantAuthorizationService>();
        _userContext= new Mock<IUserContext>();

        _handler = new UpdateRestaurantCommandHandler(
            _restaurantsRepository.Object,
            _authorizationService.Object,
            _logger.Object,
            _userContext.Object,
            _mapper.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldUpdateRestaurantsAsync()
    {
        // arrange
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand()
        {
           Id = restaurantId,
           Name="New Test Restaurant",
           Description="Test Description",
           HasDelivery=true,    
        };
        var restaurant = new Restaurant()
        {
            Id=restaurantId,    
            Name="Old Test",
            Description="Old Test Restaurant",
            HasDelivery=false,
        };

        _restaurantsRepository.Setup(r=> r.GetOneRestaurantByIdAsync(restaurantId,true))
                                .ReturnsAsync(restaurant);

        var currentUser = new CurrentUser("owner-id", "test@gmail", ["Admin"], null, null);
        _userContext.Setup(u=>u.GetCurrentUser())
            .Returns(currentUser);

        _authorizationService.Setup(a => a.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                             .Returns(true);
        _mapper.Setup(m => m.Map<Restaurant>(command))
               .Returns(restaurant);


        //act
        await _handler.Handle(command, CancellationToken.None);

        //Assert

        _restaurantsRepository.Verify(r=>r.SaveChangesAsync(), Times.Once);
        _mapper.Verify(r=>r.Map<Restaurant>(command), Times.Once);   
        
    }
    [Fact]
    public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
    {
        // arrange
        var restaurantId = 2;
        var command = new UpdateRestaurantCommand{ Id = restaurantId };
        var restaurant= new Restaurant() { Id=restaurantId };
        _restaurantsRepository.Setup(r => r.GetOneRestaurantByIdAsync(restaurantId, true))
            .ReturnsAsync((Restaurant?)null);

        var currentUser = new CurrentUser("owner-id", "test@gmail", ["Admin"], null, null);
        _userContext.Setup(u => u.GetCurrentUser())
            .Returns(currentUser);

        //Act
        Func<Task> action=async () =>await _handler.Handle(command,CancellationToken.None);

        //Assert

        await action.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id: {restaurantId} doesn't exist");
        
    }
    [Fact]
    public async Task Handle_WithUnAuthorizedUser_ShouldThrowForbidenException()
    {
        // arrange
        var restaurantId = 2;
        var command = new UpdateRestaurantCommand() { Id = restaurantId };
        var restaurant = new Restaurant() 
        {
            Id =restaurantId ,
            OwnerId="owid"
        };
        //retrieving restaurant
        _restaurantsRepository.Setup(r => r.GetOneRestaurantByIdAsync(restaurantId, true))
            .ReturnsAsync((restaurant));

        //retreiving user
        var currentUser = new CurrentUser("owner-id", "test@gmail", ["Owner"], null, null);
        _userContext.Setup(u => u.GetCurrentUser())
            .Returns(currentUser);


        _authorizationService.Setup(r => r.Authorize(restaurant, ResourceOperation.Update));
        //Act
        Func<Task> action = async () => await _handler.Handle(command, CancellationToken.None);

        //Assert

        await action.Should().ThrowAsync<ForbidenException>()
            .WithMessage($"This user :{currentUser.Email} is not authorize to perform the [{ResourceOperation.Update}] operation");

    }




}