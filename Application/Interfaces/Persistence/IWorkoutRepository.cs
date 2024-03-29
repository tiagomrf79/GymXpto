﻿using Domain.Entities.Schedule;

namespace Application.Interfaces.Persistence;

public interface IWorkoutRepository : IAsyncRepository<Workout>
{
    Task<List<Workout>> GetWorkoutsFromRoutine(Guid routineId);
}
