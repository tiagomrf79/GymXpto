using Application.Features.Routines.Commands.DeleteRoutine;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutineFeatures.Commands;

public class DeleteRoutineTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;

    public DeleteRoutineTests()
    {
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
    }

    [Fact]
    public async Task Handle_ValidRoutine_ShouldPerishInRepository()
    {
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var command = new DeleteRoutineCommand { RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab") };
        var handler = new DeleteRoutineCommandHandler(_mockRoutineRepository.Object);
        await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        recordCountAfter.ShouldBe(recordCountBefore - 1);
    }

    [Fact]
    public async Task Handle_ValidRoutine_ShouldReturnSuccess()
    {
        var command = new DeleteRoutineCommand { RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab") };
        var handler = new DeleteRoutineCommandHandler(_mockRoutineRepository.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ShouldReturnErrorMessage()
    {
        var command = new DeleteRoutineCommand { RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb") };
        var handler = new DeleteRoutineCommandHandler(_mockRoutineRepository.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
