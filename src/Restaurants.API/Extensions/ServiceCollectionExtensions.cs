using Microsoft.OpenApi.Models;
using Restaurants.API.Middleware;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;

namespace Restaurants.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
       
        // Add services to the container.

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(s =>
        {
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                In = ParameterLocation.Header,
                Name = "Authorization"
            });
            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference= new OpenApiReference()
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Name="Authorization",
                            In=ParameterLocation.Header,
                            Scheme="bearer"
                        },
                        []
                    }
            });
        });

        // DI Container - Extensions Methods calls
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<RequestTimeLoggingMiddleware>();
      
        //Inside my Program.cs - API layer
        services.AddIdentityApiEndpoints<User>();
    
    }
}
