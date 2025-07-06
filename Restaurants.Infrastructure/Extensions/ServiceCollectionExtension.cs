using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<RestaurantsDbContext>(Options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection"); 
            Options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging();
        });
    }
}
