using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Moq;

namespace Application.UnitTests.Common;

public static class WorkoutRepositoryFactory
{
    public static Mock<IWorkoutRepository> Create()
    {
        var mockWorkoutRepository = new Mock<IWorkoutRepository>();
        var workouts = GetData();

        mockWorkoutRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
            (Guid idToFind) =>
                workouts.FirstOrDefault(r => r.WorkoutId == idToFind)
            );

        mockWorkoutRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(workouts);

        mockWorkoutRepository.Setup(repo => repo.AddAsync(It.IsAny<Workout>())).ReturnsAsync(
            (Workout workoutToAdd) =>
            {
                workouts.Add(workoutToAdd);
                return workoutToAdd;
            });

        mockWorkoutRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Workout>())).Returns(
            (Workout updatedWorkout) =>
            {
                var originalWorkout = workouts.FirstOrDefault(r => r.WorkoutId == updatedWorkout.WorkoutId)!;
                originalWorkout.RoutineId = updatedWorkout.RoutineId;
                originalWorkout.Title = updatedWorkout.Title;

                return Task.CompletedTask;
            });

        mockWorkoutRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Workout>())).Returns(
            (Workout entityToDelete) =>
            {
                workouts.Remove(entityToDelete);
                return Task.CompletedTask;
            });

        return mockWorkoutRepository;
    }

    public static List<Workout> GetData()
    {
        var data = new List<Workout>
        {
            new Workout
            {
                WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d"),
                RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
                Title = "Workout A title"
            },
            new Workout
            {
                WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a"),
                RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
                Title = "Workout B title"
            }
        };

        return data;
    }
}
