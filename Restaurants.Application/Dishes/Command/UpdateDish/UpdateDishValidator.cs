using FluentValidation;

namespace Restaurants.Application.Dishes.Command.UpdateDish;

public class UpdateDishValidator : AbstractValidator<UpdateDishCommand>
{
    public UpdateDishValidator()
    {
        RuleFor(d => d.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than zero (0)");

        RuleFor(d => d.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kilocalories must be greater than zero (0)");
    }
}
