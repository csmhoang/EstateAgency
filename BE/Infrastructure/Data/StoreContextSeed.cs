using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(RepositoryContext context)
        {
            if (!context.Rooms.Any())
            {
                var roomsData = File.ReadAllText("../Infrastructure/Data/SeedData/rooms.json");
                var rooms = JsonSerializer.Deserialize<List<Room>>(roomsData);
                if (rooms != null && rooms.Any())
                {
                    context.Rooms.AddRange(rooms);
                }
            }
            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
