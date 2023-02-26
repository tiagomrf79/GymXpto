using Application.Features.Workouts.Commands.DeleteWorkout;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Workouts.Commands;

[Collection(nameof(DataCollection))]
public class DeleteWorkoutTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly IMapper _mapper;

    public DeleteWorkoutTests(TestFixture testDataFixture)
    {
        _mockWorkoutRepository = testDataFixture.MockWorkoutRepository;
        _mapper = testDataFixture.Mapper;
    }

    [Fact]
    public async Task Handle_ValidWorkout_RemovedFromWorkoutsRepo()
    {
        var handler = new DeleteWorkoutCommandHandler(_mockWorkoutRepository.Object);
        var command = new DeleteWorkoutCommand()
        {
            WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d")
        };
        int recordCountBefore = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore - 1);
        result.Success.ShouldBeTrue();

    }

    [Fact]
    public async Task Handle_NotFoundWorkout_ErrorMessageReturned()
    {
        var handler = new DeleteWorkoutCommandHandler(_mockWorkoutRepository.Object);
        var command = new DeleteWorkoutCommand()
        {
            WorkoutId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb")
        };
        int recordCountBefore = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
