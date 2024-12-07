namespace Tournament.Core.Dto.Queries;

public class TournamentPagingQueryParams : PagingQueryParams
{
    public bool IncludeGames { get; set; } = false;

}