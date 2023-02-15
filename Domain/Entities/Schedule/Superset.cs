﻿using Domain.Common;

namespace Domain.Entities.Schedule;

/// <summary>
/// One 'Superset' represents executing one set of one (normal set) or more (superset) exercises
/// For example: executing one superset of Barbell Squat and Dumbbell Lunge
/// </summary>
public class Superset : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public int Order { get; set; } //position in group of supersets
    public IList<ExerciseSet> ExercisesInSuperset { get; private set; } = new List<ExerciseSet>();
}
