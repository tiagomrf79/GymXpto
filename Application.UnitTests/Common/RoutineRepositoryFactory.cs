using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Moq;

namespace Application.UnitTests.Common;

public static class RoutineRepositoryFactory
{
    public static Mock<IRoutineRepository> Create()
    {
        var routines = new List<Routine>
        {
            new Routine
            {
                RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
                Title = "Routine A title",
                Description = "Routine A description"
            },
            new Routine
            {
                RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
                Title = "Routine B title",
                Description = "Routine B description",
                Workouts =
                {
                    new Workout
                    {
                        WorkoutId = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83"),
                        RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
                        Title = "Workout A title"
                    }
                }
            }
        };

        var mockRoutineRepository = new Mock<IRoutineRepository>();

        mockRoutineRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
           (Guid idToFind) =>
               routines.FirstOrDefault(r => r.RoutineId == idToFind)
           );

        mockRoutineRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(routines);

        mockRoutineRepository.Setup(repo => repo.AddAsync(It.IsAny<Routine>())).ReturnsAsync(
            (Routine routineToAdd) =>
            {
                routines.Add(routineToAdd);
                return routineToAdd;
            });

        mockRoutineRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Routine>())).Returns(
            (Routine updatedRoutine) =>
            {
                var originalRoutine = routines.FirstOrDefault(r => r.RoutineId == updatedRoutine.RoutineId)!;
                originalRoutine.Title = updatedRoutine.Title;
                originalRoutine.Description = updatedRoutine.Description;

                return Task.CompletedTask;
            });

        mockRoutineRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Routine>())).Returns(
            (Routine routineToDelete) =>
            {
                routines.Remove(routineToDelete);
                return Task.CompletedTask;
            });

        mockRoutineRepository.Setup(repo => repo.GetRoutineWithWorkouts()).ReturnsAsync(routines);

        return mockRoutineRepository;
    }
}
