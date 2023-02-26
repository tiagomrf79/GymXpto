using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Moq;

namespace Application.UnitTests.Common;

public class RoutineRepositoryFactory
{
    public static Mock<IRoutineRepository> GetRoutineRepository(List<Routine> routines)
    {
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
