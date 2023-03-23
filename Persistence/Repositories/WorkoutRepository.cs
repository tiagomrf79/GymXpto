using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class WorkoutRepository : BaseRepository<Workout>, IWorkoutRepository
{
    public WorkoutRepository(GymXptoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Workout>> GetWorkoutsFromRoutine(Guid routineId)
    {
        var workoutList = await _dbContext.Workouts
            .Where(w => w.RoutineId == routineId)
            .ToListAsync();

        return workoutList;
    }
}
