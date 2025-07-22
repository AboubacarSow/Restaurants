
using AutoMapper;
using FluentAssertions;
using Moq;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Dtos;

public class RestaurantAutoMapperTests
{

    
    [Fact()]
    public void CreateMap_RestaurantToRestaurantDto_MapsCorrectly()
    {
        // Arrange
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile(new RestaurantAutoMapper());
        });

        var mapper = configuration.CreateMapper();

        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "Test restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123456789",
            Address = new Address
            {
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12-345"
            }
        };

        // Act
        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

        // Assert
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.ContactEmail.Should().Be(restaurant.ContactEmail);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.Street.Should().Be(restaurant.Address?.Street);
        restaurantDto.PostalCode.Should().Be(restaurant.Address?.PostalCode);
    }
}
