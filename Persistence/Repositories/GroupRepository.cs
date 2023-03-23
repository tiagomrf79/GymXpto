using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    public GroupRepository(GymXptoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Group>> GetGroupsFromWorkout(Guid workoutId)
    {
        var groupList = await _dbContext.Groups
            .Where(g => g.WorkoutId == workoutId)
            .OrderBy(g => g.Order)
            .ToListAsync();

        return groupList;
    }
}
