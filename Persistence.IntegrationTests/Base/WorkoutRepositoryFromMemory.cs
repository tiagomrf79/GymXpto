using Persistence.Repositories;

namespace Persistence.IntegrationTests.Base;

public class WorkoutRepositoryFromMemory
{
    public static async Task<WorkoutRepository> GetWorkoutRepository()
    {
        var dbContext = await GymXptoInMemoryContext.GetInMemoryContext();
        return new WorkoutRepository(dbContext);
    }
}
