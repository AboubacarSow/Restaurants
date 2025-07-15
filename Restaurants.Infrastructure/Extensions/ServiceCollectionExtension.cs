using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestaurantsDbContext>(Options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging();// This method allows us to see sensitive data in our log

        });


        //Inside my Infrastructure Layer
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();


        services.AddScoped<IRestaurantConfig, RestaurantConfig>();
        services.AddScoped<IRestaurantsRepository,RestaurantsRepository>();
        services.AddScoped<IDishRepository,DishRepository>();
    }
}
