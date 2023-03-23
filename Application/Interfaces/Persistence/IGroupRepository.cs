using Domain.Entities.Schedule;

namespace Application.Interfaces.Persistence;

public interface IGroupRepository : IAsyncRepository<Group>
{
    Task<List<Group>> GetGroupsFromWorkout(Guid workoutId);
}
