using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

public record GetAllDishesForRestaurantQuery(int RestaurantId) : IRequest<List<DishDto>>;
