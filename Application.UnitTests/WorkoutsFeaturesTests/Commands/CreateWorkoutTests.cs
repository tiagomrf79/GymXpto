using Application.Features.Workouts.Commands.CreateWorkout;
using Application.Interfaces.Persistence;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.WorkoutsFeaturesTests.Commands;

[Collection(nameof(DataCollection))]
public class CreateWorkoutTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public CreateWorkoutTests(TestFixture testDataFixture)
    {
        _mockWorkoutRepository = testDataFixture.MockWorkoutRepository;
        _mockRoutineRepository = testDataFixture.MockRoutineRepository;
        _mapper = testDataFixture.Mapper;
    }

    [Fact]
    public async Task Handle_ValidWorkout_AddedToWorkoutsRepo()
    {
        var handler = new CreateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = "New workout title"
        };
        int recordCountBefore = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore + 1);
        result.Success.ShouldBeTrue();
        result.Workout.RoutineId.ShouldBe(command.RoutineId);
        result.Workout.Title.ShouldBe(command.Title);
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ErrorMessageReturned()
    {
        var handler = new CreateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "New workout title"
        };
        int recordCountBefore = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Handle_InvalidWorkout_ValidationErrorsReturned()
    {
        var handler = new CreateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = ""
        };
        int recordCountBefore = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }
}
