using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;
internal class RestaurantConfig(RestaurantsDbContext _context) : IRestaurantConfig
{
   
    public async Task SeedAsync()
    {
        if (await _context.Database.CanConnectAsync())
        {
            if( ! _context.Restaurants.Any())
            {
                // Seed initial data
                var restaurants= GetRestaurants();                
                 _context.Restaurants.AddRange(restaurants);
                await _context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
        [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactNumber="+44 20 7930 1234",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                },
                
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactNumber = "+44 20 7930 5668",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];
        
           return restaurants;
    }
}
