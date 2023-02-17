using Domain.Entities.Schedule;

namespace Application.Interfaces.Persistence;

public interface IRoutineRepository : IAsyncRepository<Routine>
{
    Task<Routine> GetRoutineWithWorkouts();
}
