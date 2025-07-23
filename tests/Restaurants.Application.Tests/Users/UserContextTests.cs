using Xunit;
using Restaurants.Application.Users;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Tests.Users;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        //Arrange
        var dateOfBirth = new DateOnly(1999, 08, 24);
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, "1"),
            new (ClaimTypes.Email, "test@test.com"),
            new (ClaimTypes.Role, UserRoles.Admin),
            new (ClaimTypes.Role, UserRoles.User),
            new ("Nationality", "German"),
            new ("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd")),
        };
        var user= new ClaimsPrincipal(new ClaimsIdentity(claims,"Test"));
        httpContextAccessorMock.Setup(h=>h.HttpContext).Returns(new DefaultHttpContext()
        {
            User=user,
        });
        var userContext= new UserContext(httpContextAccessorMock.Object);
        //Act
        var currentUser=userContext.GetCurrentUser();

        //Assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().Contain(UserRoles.Admin,UserRoles.User);
        currentUser.Nationality.Should().Be("German");
        currentUser.DateOfBirth.Should().Be(dateOfBirth);
    }
    [Fact]
    public void GetCurrentUser_WithHttpContextNoPresent_ThrowInvalidOperationException()
    {
        //Arrange
        var dateOfBirth = new DateOnly(1999, 08, 24);
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, "1"),
            new (ClaimTypes.Email, "test@test.com"),
            new (ClaimTypes.Role, UserRoles.Admin),
            new (ClaimTypes.Role, UserRoles.User),
            new ("Nationality", "German"),
            new ("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd")),
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
        httpContextAccessorMock.Setup(h => h.HttpContext).Returns((HttpContext)null);

        var userContext= new UserContext(httpContextAccessorMock.Object);
        //Act
        Action action=()=> userContext.GetCurrentUser();

        //Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User Context is not present");
    }
}