namespace Tournament.Core.Dto.Queries;

public class GameQueryParameters : PagingQueryParams
{
    public string? Title { get; set; }
}