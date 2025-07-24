using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Authorization.Requirements.MinimumAge;
using Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;
using Restaurants.Infrastructure.Configuration;
using Restaurants.Infrastructure.Storage;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestaurantsDbContext>(Options =>
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");
            Options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging();// This method allows us to see sensitive data in our log

        });


        //Inside my Infrastructure Layer
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()// this method adds to the User.Claims Roles, so we can perform authorization based on roles
            .AddClaimsPrincipalFactory<RestaurantsUserClaimPrincipalFactory>() //this is a custom claim which wiht we have inside of it plus default claims user Nationality and Date of Birth
            .AddEntityFrameworkStores<RestaurantsDbContext>();


        services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTypes.Nationality))
                .AddPolicy(PolicyNames.AtLeast20,
                builder =>builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.CreatedAtLeast2Restaurants,
                builder=>builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));


        services.AddScoped<IAuthorizationHandler,MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler,CreatedMultipleRestaurantsRequirementHandler>();

        services.AddScoped<IRestaurantConfig, RestaurantConfig>();
        services.AddScoped<IRestaurantsRepository,RestaurantsRepository>();
        services.AddScoped<IDishRepository,DishRepository>();
        services.AddScoped<IRestaurantAuthorizationService,RestaurantAuthorizationService>();

        //BlobStorage Configuration
        services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
