using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.IntegrationTests.Common;

public static class GymXptoContextFactory
{
    public static GymXptoDbContext Create(List<Routine> data)
    {
        var dbContextOptions = new DbContextOptionsBuilder<GymXptoDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var _dbContext = new GymXptoDbContext(dbContextOptions);

        try
        {
            _dbContext.AddRange(data);
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            //TODO: handle add data to dbcontext for tests exception (log)
            //logger.LogError(ex, $"An error occurred seeding the dbcontext with test data. Error: {ex.Message}");
        }

        return _dbContext;
    }
}
