using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        //services.AddAutoMapper(typeof(AssemblyReference).Assembly);
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AssemblyReference).Assembly)); //version 15.0.1 et plus(j'imagine)

        services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
      
        services.AddMediatR(config=>config.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }
}
