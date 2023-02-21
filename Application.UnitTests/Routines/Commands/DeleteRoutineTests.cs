using Application.Features.Routines.Commands.DeleteRoutine;
using Application.Interfaces.Persistence;
using Application.UnitTests.Mocks;
using AutoMapper;
using Domain.Entities.Schedule;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Commands;

public class DeleteRoutineTests
{
    private readonly Mock<IAsyncRepository<Routine>> _mockRoutineRepository;

    public DeleteRoutineTests()
    {
        _mockRoutineRepository = RoutineRepositoryMock.GetRoutineRepository();
    }

    [Fact]
    public async Task Handle_ValidRoutine_RemovedFromRoutinesRepo()
    {
        Guid idToDelete = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c");
        var handler = new DeleteRoutineCommandHandler(_mockRoutineRepository.Object);
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(new DeleteRoutineCommand() { RoutineId = idToDelete} , CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore - 1);
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ErrorMessageReturned()
    {
        Guid id = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");
        var handler = new DeleteRoutineCommandHandler(_mockRoutineRepository.Object);
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(new DeleteRoutineCommand() { RoutineId = id }, CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

}
