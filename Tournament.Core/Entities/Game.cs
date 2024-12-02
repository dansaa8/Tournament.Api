using System.ComponentModel.DataAnnotations.Schema;

namespace Tournament.Core.Entities;

public class Game
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime Time { get; set; }

    public int TournamentId { get; set; } // fk to TournamentDetails

    [ForeignKey("TournamentId")] // nav prop
    public TournamentDetails Details { get; set; } = new TournamentDetails();

}