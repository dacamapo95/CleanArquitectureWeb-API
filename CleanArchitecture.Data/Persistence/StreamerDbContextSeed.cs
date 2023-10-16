using CleanArchitecture.Domain;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext streamerDbContext)
                                          
        {
            if (!streamerDbContext.Streamers!.Any())
            {
                streamerDbContext.AddRange(InitializeStreamers());
                await streamerDbContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Streamer> InitializeStreamers()
        {
            return new List<Streamer>()
            {
                new Streamer { Name = "Ninja", Url = "https://www.twitch.tv/ninja" , CreatedBy = "dacamapo"},
                new Streamer { Name = "Shroud", Url = "https://www.twitch.tv/shroud", CreatedBy = "dacamapo" },
                new Streamer { Name = "Pokimane", Url = "https://www.twitch.tv/pokimane", CreatedBy = "dacamapo" },
                new Streamer { Name = "TimTheTatman", Url = "https://www.twitch.tv/timthetatman" , CreatedBy = "dacamapo"},
                new Streamer { Name = "Valkyrae", Url = "https://www.twitch.tv/valkyrae" , CreatedBy = "dacamapo" }
            };
        }
    }
}
