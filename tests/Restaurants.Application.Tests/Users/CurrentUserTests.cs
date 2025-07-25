using Xunit;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Tests.Users;
public class CurrentUserTests
{

    //TestMethod_Scenario_ExpectedResult
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        //Arrange
        var current = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null!, null);
        //Act
        var isInRole = current.IsInRole(roleName);
        //Assert
        //Xunit.Assert.True(isInRole);
        isInRole.Should().BeTrue();
    }
    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        //Arrange
        var current = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null!, null);
        //Act
        var isInRole = current.IsInRole(UserRoles.Owner);
        //Assert
        //Xunit.Assert.True(isInRole);
        isInRole.Should().BeFalse();
    }
    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        //Arrange
        var current = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null!, null);
        //Act
        var isInRole = current.IsInRole(UserRoles.Admin.ToLower());
        //Assert
        //Xunit.Assert.True(isInRole);
        isInRole.Should().BeFalse();
    }
}