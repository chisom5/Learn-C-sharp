namespace PolarisEcl.Application.Common.Dtos;

public class PageQuery
{
    public int PageNum {get; set;} = 1;
    public int PageSize {get; set;} = 2;
    public string? Search {get; set;}
    public string? SortBy {get; set;}
}

public class PageResponse<T>
{
     public IReadOnlyList<T> Data { get; init; } = [];
    public int PageNum { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public int TotalRecords { get; init; }
    public bool HasNextPage => PageNum < TotalPages;
    public bool HasPreviousPage => PageNum > 1;
}