using Persistence.Repositories;

namespace Persistence.IntegrationTests.Base;

public class RoutineRepositoryFromMemory
{
    public static async Task<RoutineRepository> GetRoutineRepository()
    {
        var dbContext = await GymXptoInMemoryContext.GetInMemoryContext();
        return new RoutineRepository(dbContext);
    }
}
