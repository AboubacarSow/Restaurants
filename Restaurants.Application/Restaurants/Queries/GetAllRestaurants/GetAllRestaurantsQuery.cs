using FluentValidation;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities.RequestFeatures;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<(IEnumerable<RestaurantDto>, MetaData)>
{
    public bool TrackChanges { get; set; }
    public string? SearchTerm {  get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

}
public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowedPageSizes = [5, 10, 15, 20, 25, 30];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(q => q.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be greater or equal to 1");
        RuleFor(q => q.PageSize)
            .Must(value => allowedPageSizes.Contains(value))
            .WithMessage($"PagSize mut be in [{String.Join(',',allowedPageSizes.Select(r=>r.ToString()))}]");
    }
}
