using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.API.Middleware;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.API.Tests.Middleware;

public class ErrorHandlingMiddlewareTests
{
    private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
    private readonly ErrorHandlingMiddleware _errorHandlingMiddleware;

    public ErrorHandlingMiddlewareTests()
    {
        _loggerMock= new Mock<ILogger<ErrorHandlingMiddleware>>();
        _errorHandlingMiddleware= new ErrorHandlingMiddleware(_loggerMock.Object);
    }
    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
    {
        //Arrange
        var context = new DefaultHttpContext();
        var _delegate= new Mock<RequestDelegate>();

        _delegate.Setup(n => n.Invoke(context));
        //Act
        await _errorHandlingMiddleware.InvokeAsync(context, _delegate.Object);

        //Assert
        _delegate.Verify(n=>n.Invoke(context), Times.Once());
    }
    [Fact()]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCodeTo404AndWriteExceptionMessage()
    {
        //Arrange
        var context = new DefaultHttpContext();
        var responseBodyStream= new MemoryStream();
        context.Response.Body= responseBodyStream;  
        var notFoundException = new NotFoundException("Restaurant", "2");
        //Act
       
        await _errorHandlingMiddleware.InvokeAsync(context,_ => throw notFoundException);
        //Assert
        string msg = $"Resource not found: {notFoundException.Message}";
        context.Response.StatusCode.Should().Be(404);

        responseBodyStream.Seek(0, SeekOrigin.Begin);   
        var reader= new StreamReader(responseBodyStream);
        var responseBody= reader.ReadToEnd();

        var expectedResponse= JsonSerializer.Serialize(new
        {
            statusCode = 404,
            message = notFoundException.Message
        });

        responseBody.Should().Be(expectedResponse); 
        
    }

    [Fact()]
    public async Task InvokeAsync_WhenForbidenExceptionThrown_ShouldSetStatusCodeTo403AndWriteExceptionMessage()
    {
        //Arrange
        var context= new DefaultHttpContext();  
        var responseStream= new MemoryStream(); 
        context.Response.Body= responseStream;

        var forbidenException = new ForbidenException(nameof(Restaurant), "3");

        context.Response.StatusCode = 403;

        //Act
         await _errorHandlingMiddleware.InvokeAsync(context, _ =>throw forbidenException);

        //Assert

        //Set status code to 404
        context.Response.StatusCode.Should().Be(403);

        //writing the message
        responseStream.Seek(0, SeekOrigin.Begin);
        var reader= new StreamReader(responseStream);
        var responseBody= reader.ReadToEnd();

        var expectedResponse = JsonSerializer.Serialize(new
        {
            statusCode = 403,
            message = forbidenException.Message
        });

        responseBody.Should().Be(expectedResponse);

    }

    [Fact()]
    public async Task InvokeAsync_WhenGeneralExceptionThrown_ShouldSetStatusCodeTo500()
    {
        //Arrange
        var context= new DefaultHttpContext();
        var exception = new Exception();

        //Act
        context.Response.StatusCode = 500;

        await _errorHandlingMiddleware.InvokeAsync(context,_ =>throw exception);

        //Assert

        context.Response.StatusCode.Should().Be(500);

    }
}