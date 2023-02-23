using Application.Interfaces.Persistence;
using Domain.Entities.Schedule;
using Moq;

namespace Application.UnitTests.Mocks;

public class RoutineRepositoryMock
{
    public static Mock<IRoutineRepository> GetRoutineRepository()
    {
        var routines = new List<Routine>
        {
            new Routine
            {
                RoutineId = new Guid("5f5606f9-8e09-47d8-8fe0-6cbad8ab49e5"),
                Title = "Rotina do BestOf",
                Description = "um treino diferente para cada dia da semana"
            },
            new Routine
            {
                RoutineId = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c"),
                Title = "Rotina do BodyStation",
                Description = "um treino de lower body e um treino de upper body"
            },
            new Routine
            {
                RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
                Title = "A minha rotina",
                Description = "treino construído com base em exemplos retirados da internet"
            }
        };

        var mockRoutineRepository = new Mock<IRoutineRepository>();

        mockRoutineRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
            (Guid id) =>
                routines.FirstOrDefault(r => r.RoutineId == id)
            );

        mockRoutineRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(routines);

        mockRoutineRepository.Setup(repo => repo.AddAsync(It.IsAny<Routine>())).ReturnsAsync(
            (Routine routine) =>
            {
                routines.Add(routine);
                return routine;
            });

        mockRoutineRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Routine>())).Returns(
            (Routine routine) =>
            {
                var routineToUpdate = routines.FirstOrDefault(r => r.RoutineId == routine.RoutineId);

                if (routineToUpdate != null)
                {
                    routineToUpdate.Title = routine.Title;
                    routineToUpdate.Description = routine.Description;
                }

                return Task.CompletedTask;
            });

        mockRoutineRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Routine>())).Returns(
            (Routine routine) =>
            {
                routines.Remove(routine);
                return Task.CompletedTask;
            });

        return mockRoutineRepository;
    }
}
