namespace Tournament.Core.Dto.Queries;

public class TournamentQueryParams
{
    public bool IncludeGames { get; set; } = false;

    private uint _pageSize = 20;
    public uint PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 110 ? 100 : value;
    } 

    public uint PageNumber { get; set; } = 1;
}