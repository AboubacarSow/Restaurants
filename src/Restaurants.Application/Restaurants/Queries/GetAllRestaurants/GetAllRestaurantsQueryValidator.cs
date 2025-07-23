using FluentValidation;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] allowedPageSizes = [5, 10, 15, 20, 25, 30];
    private readonly string[] allowedSortByColumns = 
        [
                nameof(Restaurant.Name),
                nameof(Restaurant.Description),
                nameof(Restaurant.Category)
        ];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(q => q.pageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("pageNumber must be greater or equal to 1");
        RuleFor(q => q.pageSize)
            .Must(value => allowedPageSizes.Contains(value))
            .WithMessage($"PagSize mut be in [{String.Join(',',allowedPageSizes.Select(r=>r.ToString()))}]");

        RuleFor(r => r.sortBy)
            .Must(value => allowedSortByColumns.Contains(value))
            .When(r => r.sortBy != null)
            .WithMessage($"sortBy is optional - or must be in [{String.Join(',', allowedSortByColumns.Select(r => r.ToString()))}] ");
   
    }
}
