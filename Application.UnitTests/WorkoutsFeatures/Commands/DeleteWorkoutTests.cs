using Application.Features.Workouts.Commands.DeleteWorkout;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.WorkoutsFeatures.Commands;

public class DeleteWorkoutTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly IMapper _mapper;

    public DeleteWorkoutTests()
    {
        _mockWorkoutRepository = WorkoutRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ValidWorkout_ShouldPerishInRepository()
    {
        int recordCountBefore = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        var command = new DeleteWorkoutCommand { WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d") };
        var handler = new DeleteWorkoutCommandHandler(_mockWorkoutRepository.Object);
        await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        recordCountAfter.ShouldBe(recordCountBefore - 1);
    }

    [Fact]
    public async Task Handle_ValidWorkout_ShouldReturnSuccess()
    {
        var command = new DeleteWorkoutCommand { WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d") };
        var handler = new DeleteWorkoutCommandHandler(_mockWorkoutRepository.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_NotFoundWorkout_ShouldReturnErrorMessage()
    {
        var command = new DeleteWorkoutCommand { WorkoutId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb") };
        var handler = new DeleteWorkoutCommandHandler(_mockWorkoutRepository.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
