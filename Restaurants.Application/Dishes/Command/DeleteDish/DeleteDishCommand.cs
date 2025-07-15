using MediatR;

namespace Restaurants.Application.Dishes.Command.DeleteDish;

public record DeleteDishCommand(int Id,int RestaurantId):IRequest;
