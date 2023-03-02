using Application.Features.Workouts.Commands.CreateWorkout;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.WorkoutFeatures.Commands;

public class CreateWorkoutTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public CreateWorkoutTests()
    {
        _mockWorkoutRepository = WorkoutRepositoryFactory.Create();
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ValidWorkout_ShouldPersistInRepository()
    {
        int recordCountBefore = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
            Title = "New workout title"
        };
        var handler = new CreateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockWorkoutRepository.Object.ListAllAsync()).Count;

        recordCountAfter.ShouldBe(recordCountBefore + 1);
    }

    [Fact]
    public async Task Handle_ValidWorkout_ShouldReturnAddedRoutine()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
            Title = "New workout title"
        };
        var handler = new CreateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.Workout.ShouldNotBeNull();
        result.Workout.RoutineId.ShouldBe(command.RoutineId);
        result.Workout.Title.ShouldBe(command.Title);
    }

    [Fact]
    public async Task Handle_InvalidWorkout_ShouldReturnValidationErrors()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
            Title = ""
        };
        var handler = new CreateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ShouldReturn()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "New workout title"
        };
        var handler = new CreateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
