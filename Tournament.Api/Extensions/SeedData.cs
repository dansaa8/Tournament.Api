﻿using Microsoft.EntityFrameworkCore;
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

                    // Create games
                    var games = GenerateGames(tournaments);
                    db.Games.AddRange(games);
                    await db.SaveChangesAsync(); // Save games
                }
                catch (Exception ex)
                {
                    throw new Exception("Error seeding the data", ex);
                }
            }
        }

        private static List<TournamentDetails> GenerateTournaments()
        {
            return new List<TournamentDetails>
            {
                new TournamentDetails
                {
                    Title = "Winter Championship 2024",
                    StartDate = new DateTime(2024, 1, 15)
                },
                new TournamentDetails
                {
                    Title = "Spring Invitational 2024",
                    StartDate = new DateTime(2024, 4, 10)
                },
                new TournamentDetails
                {
                    Title = "Summer Clash 2024",
                    StartDate = new DateTime(2024, 7, 5)
                }
            };
        }

        private static List<Game> GenerateGames(List<TournamentDetails> tournaments)
        {
            var games = new List<Game>();

            var tournament1 = tournaments[0];
            var tournament2 = tournaments[1];
            var tournament3 = tournaments[2];

            games.Add(new Game
            {
                Title = "Winter Championship Opening Match",
                Time = new DateTime(2024, 1, 15, 10, 0, 0),
                TournamentId = tournament1.Id
            });

            games.Add(new Game
            {
                Title = "Winter Championship Semi-Final",
                Time = new DateTime(2024, 1, 18, 14, 30, 0),
                TournamentId = tournament1.Id
            });

            games.Add(new Game
            {
                Title = "Spring Invitational Group Stage",
                Time = new DateTime(2024, 4, 10, 9, 0, 0),
                TournamentId = tournament2.Id
            });

            games.Add(new Game
            {
                Title = "Spring Invitational Final Match",
                Time = new DateTime(2024, 4, 15, 17, 0, 0),
                TournamentId = tournament2.Id
            });

            games.Add(new Game
            {
                Title = "Summer Clash Opening Round",
                Time = new DateTime(2024, 7, 5, 12, 0, 0),
                TournamentId = tournament3.Id
            });

            games.Add(new Game
            {
                Title = "Summer Clash Championship Match",
                Time = new DateTime(2024, 7, 10, 16, 0, 0),
                TournamentId = tournament3.Id
            });

            return games;
        }
    }
}