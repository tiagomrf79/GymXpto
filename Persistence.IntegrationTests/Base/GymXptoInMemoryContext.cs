using Microsoft.EntityFrameworkCore;

namespace Persistence.IntegrationTests.Base;

public class GymXptoInMemoryContext
{
    public static async Task<GymXptoDbContext> GetInMemoryContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<GymXptoDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var _dbContext = new GymXptoDbContext(dbContextOptions);

        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();

        return _dbContext;
    }
}
