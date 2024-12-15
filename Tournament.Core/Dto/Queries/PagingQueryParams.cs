namespace Tournament.Core.Dto.Queries;

public class PagingQueryParams
{
    
    private int _pageSize = 20;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 110 ? 100 : value;
    } 

    public int PageNumber { get; set; } = 1;
}