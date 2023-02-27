using Application.Features.Routines.Commands.DeleteRoutine;
using Application.Interfaces.Persistence;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Commands;

[Collection(nameof(DataCollection))]
public class DeleteRoutineTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;

    public DeleteRoutineTests(TestFixture testDataFixture)
    {
        _mockRoutineRepository = testDataFixture.MockRoutineRepository;
    }

    [Fact]
    public async Task Handle_ValidRoutine_RemovedFromRoutinesRepo()
    {
        Guid idToDelete = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab");
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
        Guid idToDelete = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");
        var handler = new DeleteRoutineCommandHandler(_mockRoutineRepository.Object);
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(new DeleteRoutineCommand() { RoutineId = idToDelete }, CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

}
