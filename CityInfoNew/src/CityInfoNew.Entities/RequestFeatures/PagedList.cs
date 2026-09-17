namespace CityInfoNew.Entities.RequestFeatures;

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public MetaData MetaData { get; }

    public PagedList(
    List<T> items,
    int totalItemCount,
    int pageNumber,
    int pageSize)
    {
        MetaData = new MetaData
        {
            TotalItemCount = totalItemCount,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize)
        };

        Items = items.AsReadOnly();
    }
}