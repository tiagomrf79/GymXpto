﻿namespace Application.Features.Workouts.Commands.CreateWorkout;

public class CreateWorkoutDto
{
    public Guid WorkoutId { get; set; }
    public Guid RoutineId { get; set; }
    public string Title { get; set; } = string.Empty;

}