namespace YK.ORM.Contract;

public class PaginationFilter : BaseFilter
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 20;

    public string[]? OrderBy { get; set; }
}

public static class PaginationFilterExtensions
{
    public static bool HasOrderBy(this PaginationFilter filter) =>
        filter.OrderBy?.Any() is true;
}
