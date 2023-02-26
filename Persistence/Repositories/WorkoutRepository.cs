using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;

namespace Persistence.Repositories;

public class WorkoutRepository : BaseRepository<Workout>, IWorkoutRepository
{
    public WorkoutRepository(GymXptoDbContext dbContext) : base(dbContext)
    {
    }
}
