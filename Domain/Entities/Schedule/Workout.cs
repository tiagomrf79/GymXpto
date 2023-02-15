using Domain.Common;
using Domain.Entities.Scheduling;

namespace Domain.Entities.Schedule;

/// <summary>
/// One 'Workout' represents the sequence of exercises done in one gym session
/// </summary>
public class Workout : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;
    public IList<Group> ExerciseSequence { get; private set; } = new List<Group>();
}