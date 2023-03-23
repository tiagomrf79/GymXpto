using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ExerciseSetRepository : BaseRepository<ExerciseSet>, IExerciseSetRepository
{
    public ExerciseSetRepository(GymXptoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<ExerciseSet>> GetExerciseSetsFromSuperset(Guid supersetId)
    {
        var exerciseSetList = await _dbContext.ExerciseSets
            .Where(e => e.SupersetId == supersetId)
            .OrderBy(e => e.Order)
            .ToListAsync();

        return exerciseSetList;
    }
}
