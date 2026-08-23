namespace CityInfo.Entities.RequestFeatures;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; private set; }
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            PageSize = pageSize,
            TotalCount = count
        };
        AddRange(items);
    }
}
