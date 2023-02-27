using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.IntegrationTests.Common;

public static class GymXptoContextFactory
{
    public static GymXptoDbContext Create(List<Routine> data)
    {
        var dbContextOptions = new DbContextOptionsBuilder<GymXptoDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var _dbContext = new GymXptoDbContext(dbContextOptions);
        
        _dbContext.AddRange(data);
        _dbContext.SaveChanges();
        
        return _dbContext;
    }
}
