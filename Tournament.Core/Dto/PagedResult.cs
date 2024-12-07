namespace Tournament.Core.Dto;

public class PagedResult<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>(); // Requested Data
    public int TotalItems { get; set; } // Total number of records of given type
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize); // Total pages based on page size
    public int PageNumber { get; set; } // Requested page number 
    public int PageSize { get; set; } // Number of items per page

    public bool HaxNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
}