namespace Restaurants.Domain.Entities.RequestFeatures;

public class MetaData
{
    public int CurrentPage {  get; set; }
    public int TotalCount {  get; set; }
    public int ToTalPage {  get; set; }
    public int PageSize {  get; set; }
    public bool HasPreview => CurrentPage > 1;
    public bool HasNext => CurrentPage < ToTalPage;
}
