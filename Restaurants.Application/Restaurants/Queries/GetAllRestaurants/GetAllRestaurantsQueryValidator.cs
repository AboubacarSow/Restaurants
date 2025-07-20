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
        RuleFor(q => q.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be greater or equal to 1");
        RuleFor(q => q.PageSize)
            .Must(value => allowedPageSizes.Contains(value))
            .WithMessage($"PagSize mut be in [{String.Join(',',allowedPageSizes.Select(r=>r.ToString()))}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumns.Contains(value))
            .When(r => r.SortBy != null)
            .WithMessage($"SortBy is optional - or must be in [{String.Join(',', allowedSortByColumns.Select(r => r.ToString()))}] ");
   
    }
}
