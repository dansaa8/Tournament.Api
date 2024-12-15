namespace Tournament.Presentation.Filters;

public class TournamentMaxGamesViolationException : Exception
{
    public TournamentMaxGamesViolationException(string message) : base(message)
    {
    }
}