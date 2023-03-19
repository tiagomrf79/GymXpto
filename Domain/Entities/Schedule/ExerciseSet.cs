using Domain.Common;

namespace Domain.Entities.Schedule;

/// <summary>
/// One 'ExerciseSet' represents executing one set of one exercise
/// For example: executing one set of Barbell Squat with 12 repetitions
/// </summary>
public class ExerciseSet : AuditableEntity
{
    public Guid ExerciseSetId { get; set; }
    public Guid SupersetId { get; set; }
    public int Order { get; set; } //position in superset
    public int TargetRepetitions { get; set; }
    public Guid ExerciseId { get; set; }
}