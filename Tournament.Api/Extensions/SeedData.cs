using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using System.Collections.Generic;
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

                // Check if data already exists, if yes, no need to seed.
                if (await db.TournamentDetails.AnyAsync()) return;

                try
                {
                    // Create tournaments
                    var tournaments = GenerateTournaments();
                    db.TournamentDetails.AddRange(tournaments);
                    await db.SaveChangesAsync(); // Save tournaments to get their Ids

                    // Now tournaments have their Ids populated after saving
                    // Create games and link them with the correct TournamentId
                    //var games = GenerateGames(tournaments);
                    //db.Games.AddRange(games);
                    //await db.SaveChangesAsync(); // Save games
                }
                catch (Exception ex)
                {
                    throw new Exception("Error seeding the data", ex);
                }
            }
        }

        private static List<TournamentDetails> GenerateTournaments()
        {
            var tournaments = new List<TournamentDetails>();
            var random = new Random();

            for (int i = 1; i <= 30; i++)
            {
                // Generate a random number of games (0 to 5)
                int gameCount = random.Next(0, 6);
                var games = new List<Game>();

                for (int j = 1; j <= gameCount; j++)
                {
                    games.Add(new Game
                    {
                        Title = $"Game {j} of Tournament {i}",
                        Time = DateTime.Now.AddDays(random.Next(0, 30)) // Random time within the next 30 days
                    });
                }

                tournaments.Add(new TournamentDetails
                {
                    Title = $"Tournament {i}",
                    StartDate = DateTime.Now.AddDays(random.Next(1, 365)), // Random start date within the next year
                    Games = games
                });
            }

            return tournaments;
        }
    }
}