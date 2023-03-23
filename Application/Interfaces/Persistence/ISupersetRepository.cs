using Domain.Entities.Schedule;

namespace Application.Interfaces.Persistence;

public interface ISupersetRepository : IAsyncRepository<Superset>
{
    Task<List<Superset>> GetSupersetsFromGroup(Guid groupId);
}
