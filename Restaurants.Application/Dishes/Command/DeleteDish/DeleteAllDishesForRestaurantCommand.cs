using MediatR;

namespace Restaurants.Application.Dishes.Command.DeleteDish;

public record DeleteAllDishesForRestaurantCommand(int RestaurantId):IRequest;
