using Application.Features.Workouts.Commands.UpdateWorkout;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Workouts.Commands;

[Collection(nameof(DataCollection))]
public class UpdateWorkoutTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public UpdateWorkoutTests(TestFixture testDataFixture)
    {
        _mockWorkoutRepository = testDataFixture.MockWorkoutRepository;
        _mockRoutineRepository = testDataFixture.MockRoutineRepository;
        _mapper = testDataFixture.Mapper;
    }

    [Fact]
    public async Task Handle_ValidWorkout_UpdatedInWorkoutsRepo()
    {
        var handler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var command = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a"),
            RoutineId = new Guid("d905ff6c-8bf3-4f9f-a275-598e725c6129"),
            Title = "Updated workout title"
        };
        int recordCountBefore = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeTrue();
        result.Workout.WorkoutId.ShouldBe(command.WorkoutId);
        result.Workout.RoutineId.ShouldBe(command.RoutineId);
        result.Workout.Title.ShouldBe(command.Title);
    }

    [Fact]
    public async Task Handle_NotFoundWorkout_ErrorMessageReturned()
    {
        var handler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var command = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            RoutineId = new Guid("d905ff6c-8bf3-4f9f-a275-598e725c6129"),
            Title = "Updated workout title"
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ErrorMessageReturned()
    {
        var handler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var command = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a"),
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated workout title"
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Handle_InvalidWorkout_ValidationErrorsReturned()
    {
        var handler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var command = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a"),
            RoutineId = new Guid("d905ff6c-8bf3-4f9f-a275-598e725c6129"),
            Title = ""
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }
}
