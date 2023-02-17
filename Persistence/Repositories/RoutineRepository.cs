using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;

namespace Persistence.Repositories;

public class RoutineRepository : BaseRepository<Routine>, IRoutineRepository
{
    public RoutineRepository(GymXptoDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Routine> GetRoutineWithWorkouts()
    {
        //TODO: implement query to return routine with workout list
        throw new NotImplementedException();
    }
}
