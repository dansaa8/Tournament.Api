using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tournament.Api.Extensions
{
    public static class SeedData
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<TournamentApiContext>();

                await db.Database.MigrateAsync();

                // Kolla om data redan finns, isåfall behövs ingen seedning.
                if (await db.TournamentDetails.AnyAsync()) return;

                try
                {
                    var tournaments = GenerateTournamentsWithGames();
                    db.AddRange(tournaments);

                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error seeding the data", ex);
                }
            }
        }

        private static IEnumerable<TournamentDetails> GenerateTournamentsWithGames()
        {
            return new List<TournamentDetails>
            {
                new TournamentDetails
                {
                    Title = "Winter Championship 2024",
                    StartDate = new DateTime(2024, 1, 15),
                    Games = new List<Game>
                    {
                        new Game { Title = "Opening Match", Time = new DateTime(2024, 1, 15, 10, 0, 0) },
                        new Game { Title = "Semi-Final", Time = new DateTime(2024, 1, 18, 14, 30, 0) }
                    }
                },
                new TournamentDetails
                {
                    Title = "Spring Invitational 2024",
                    StartDate = new DateTime(2024, 4, 10),
                    Games = new List<Game>
                    {
                        new Game { Title = "Group Stage", Time = new DateTime(2024, 4, 10, 9, 0, 0) },
                        new Game { Title = "Final Match", Time = new DateTime(2024, 4, 15, 17, 0, 0) }
                    }
                },
                new TournamentDetails
                {
                    Title = "Summer Clash 2024",
                    StartDate = new DateTime(2024, 7, 5),
                    Games = new List<Game>
                    {
                        new Game { Title = "Opening Round", Time = new DateTime(2024, 7, 5, 12, 0, 0) },
                        new Game { Title = "Championship Match", Time = new DateTime(2024, 7, 10, 16, 0, 0) }
                    }
                },
                new TournamentDetails
                {
                    Title = "Autumn League 2024",
                    StartDate = new DateTime(2024, 10, 20),
                    Games = new List<Game>
                    {
                        new Game { Title = "Round Robin", Time = new DateTime(2024, 10, 20, 8, 0, 0) },
                        new Game { Title = "Playoffs", Time = new DateTime(2024, 10, 25, 15, 30, 0) }
                    }
                }
            };
        }
    }
}
