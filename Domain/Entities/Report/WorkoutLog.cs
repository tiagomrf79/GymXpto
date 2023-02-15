using Domain.Common;

namespace Domain.Entities.Report;

/// <summary>
/// One 'WorkoutLog' represents one gym session done
/// </summary>
public class WorkoutLog : AuditableEntity
{
    public Guid Id { get; set; }
    public string WorkoutTitle { get; set; } = string.Empty;
    public string RoutineTitle { get; set; } = string.Empty;
    public DateTime WorkoutStartDate { get; set; }
    public DateTime WorkoutEndDate { get; set; }
    public IList<ExerciseLog> ExercisesDone { get; private set; } = new List<ExerciseLog>();
}
