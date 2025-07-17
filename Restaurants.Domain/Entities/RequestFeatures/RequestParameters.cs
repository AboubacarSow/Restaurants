using Restaurants.Domain.Entities.RequestFeatures;

namespace Restaurants.Domain.Entities.RequestParameters;

public class RequestParameters
{
    public string? SearchTerm {  get; set; }
    public SortDirection SortDirection { get; set; }
    public string? OrderBy {  get; set; }
    public int? PageSize { get; set; }
    public int PageNumber {  get; set; }
    public int? TotalPage { get; set; }

}
