using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishById;

public  record GetDishByIdForRestaurantQuery(int Id,int RestaurantId) : IRequest<DishDto>;

