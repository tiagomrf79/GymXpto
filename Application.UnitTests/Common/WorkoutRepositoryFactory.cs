using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Moq;

namespace Application.UnitTests.Common;

public class WorkoutRepositoryFactory
{
    public static Mock<IWorkoutRepository> Create(List<Workout> workouts)
    {
        var mockRepository = new Mock<IWorkoutRepository>();

        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
            (Guid idToFind) =>
                workouts.FirstOrDefault(r => r.WorkoutId == idToFind)
            );

        mockRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(workouts);

        mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Workout>())).ReturnsAsync(
            (Workout workoutToAdd) =>
            {
                workouts.Add(workoutToAdd);
                return workoutToAdd;
            });

        mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Workout>())).Returns(
            (Workout updatedWorkout) =>
            {
                var originalWorkout = workouts.FirstOrDefault(r => r.WorkoutId == updatedWorkout.WorkoutId)!;
                originalWorkout.RoutineId = updatedWorkout.RoutineId;
                originalWorkout.Title = updatedWorkout.Title;

                return Task.CompletedTask;
            });

        mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Workout>())).Returns(
            (Workout entityToDelete) =>
            {
                workouts.Remove(entityToDelete);
                return Task.CompletedTask;
            });

        return mockRepository;
    }
}
