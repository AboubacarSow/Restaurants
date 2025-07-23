namespace Restaurants.Domain.Entities.RequestFeatures;

public class PagedList<T>:List<T>
{
    public MetaData MetaData { get; set; }  
    public PagedList(IEnumerable<T> items,int count,int pageSize,int pageNumber)
    {
        MetaData = new MetaData()
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = count,
            ToTalPage = (int)Math.Ceiling(count/(double)pageSize),
        };
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source,int pageSize,int pageNumber)
    {
        int count=source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return new PagedList<T>(items, count, pageSize, pageNumber);
    }
}
