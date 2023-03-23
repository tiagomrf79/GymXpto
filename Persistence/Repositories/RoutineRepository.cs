using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class RoutineRepository : BaseRepository<Routine>, IRoutineRepository
{
    public RoutineRepository(GymXptoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Routine>> GetRoutineWithWorkouts()
    {
        var allRoutines = await _dbContext.Routines
            .Include(r => r.Workouts)
            .ToListAsync();
        
        return allRoutines;
    }
}
