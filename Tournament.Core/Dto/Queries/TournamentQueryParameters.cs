namespace Tournament.Core.Dto.Queries;

public class TournamentQueryParameters : PagingQueryParams
{
    public bool IncludeGames { get; set; } = false;
}