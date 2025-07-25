﻿using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Restaurants.API.Middleware;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
       
        // Add services to the container.

        services.AddControllers(config=>
        {
            config.CacheProfiles.Add("3mins",new CacheProfile() { Duration=180});
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Restaurants API",
                Version = "v1",
                Description = @"A RESTful Web API for managing restaurants and their associated data.

                    This API provides endpoints to:
                    - Retrieve the list of restaurants (public access)
                    - Search, filter, and paginate restaurant results
                    - Upload and manage logo images for restaurants
                    - Perform create, update, and delete operations (requires authentication)

                    Versioning is supported via URL path (e.g. /api/v1/restaurants), enabling backward-compatible evolution of the API.

                    This API is intended for consumption by web and mobile clients. Role-based authorization is enforced where applicable.",
                Contact = new OpenApiContact
                {
                    Email = "aboubacarsow2004@gmail.com",
                    Name = "ABOUBACAR SOW",
                }
            });


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


     //Configure Caching
     services.AddResponseCaching();

      services.AddHttpCacheHeaders(expressionOptions =>
      {
          expressionOptions.CacheLocation = CacheLocation.Public;
          expressionOptions.MaxAge = 100;
      },
      validations =>
      {
          validations.MustRevalidate = false;
      });

        // Configure Api Versioning
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });
      //Inside my Program.cs - API layer
      services.AddIdentityApiEndpoints<User>();
    
    }
}
