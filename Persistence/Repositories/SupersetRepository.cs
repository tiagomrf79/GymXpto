using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class SupersetRepository : BaseRepository<Superset>, ISupersetRepository
{
    public SupersetRepository(GymXptoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Superset>> GetSupersetsFromGroup(Guid groupId)
    {
        var supersetList = await _dbContext.Supersets
            .Where(s => s.GroupId == groupId)
            .OrderBy(s => s.Order)
            .ToListAsync();

        return supersetList;
    }
}
