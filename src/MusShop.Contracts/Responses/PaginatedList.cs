namespace MusShop.Contracts.Responses;

public class PaginatedList<TEntity>
{
    public IEnumerable<TEntity> Items { get; init; }
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public int TotalRecords { get; set; }

    public bool HasNextPage => (PageIndex + 1) * PageSize < TotalRecords;
    public bool HasPreviousPage => PageIndex > 0;

    public PaginatedList(List<TEntity> items, int? pageIndex, int totalRecords, int? pageSize)
    {
        Items = items;
        PageIndex = pageIndex ?? 0;
        PageSize = pageSize is <= 0 or null ? 6 : (int)pageSize;
        TotalPages = (int)Math.Ceiling((decimal)totalRecords / (pageSize is <= 0 or null ? 6 : 1));
        TotalRecords = totalRecords;
    }
}