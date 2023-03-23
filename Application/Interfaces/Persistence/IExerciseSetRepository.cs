using Domain.Entities.Schedule;

namespace Application.Interfaces.Persistence;

public interface IExerciseSetRepository : IAsyncRepository<ExerciseSet>
{
    Task<List<ExerciseSet>> GetExerciseSetsFromSuperset(Guid supersetId);
}
