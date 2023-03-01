using Application.Features.Workouts.Commands.UpdateWorkout;
using Application.Features.Workouts.Queries.GetWorkoutDetail;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.WorkoutsFeatures.Commands;

public class UpdateWorkoutTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public UpdateWorkoutTests()
    {
        _mockWorkoutRepository = WorkoutRepositoryFactory.Create();
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ValidWorkout_ShouldPersistInRepository()
    {
        var updateCommand = new UpdateWorkoutCommand
        {
            WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d"),
            RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
            Title = "Updated workout title"
        };
        var updateHandler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        await updateHandler.Handle(updateCommand, CancellationToken.None);

        var findQuery = new GetWorkoutDetailQuery { WorkoutId = updateCommand.WorkoutId };
        var findHandler = new GetWorkoutDetailQueryHandler(_mockWorkoutRepository.Object, _mapper);
        var findResult = await findHandler.Handle(findQuery, CancellationToken.None);

        findResult.Workout.ShouldNotBeNull();
        findResult.Workout.WorkoutId.ShouldBe(updateCommand.WorkoutId);
        findResult.Workout.RoutineId.ShouldBe(updateCommand.RoutineId);
        findResult.Workout.Title.ShouldBe(updateCommand.Title);
    }

    [Fact]
    public async Task Handle_ValidWorkout_ShouldReturnUpdatedWorkout()
    {
        var command = new UpdateWorkoutCommand
        {
            WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d"),
            RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
            Title = "Updated workout title"
        };
        var handler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.Workout.ShouldNotBeNull();
        result.Workout.WorkoutId.ShouldBe(command.WorkoutId);
        result.Workout.RoutineId.ShouldBe(command.RoutineId);
        result.Workout.Title.ShouldBe(command.Title);
    }

    [Fact]
    public async Task Handle_InvalidWorkout_ShouldReturnValidationErrors()
    {
        var command = new UpdateWorkoutCommand
        {
            WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d"),
            RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
            Title = string.Empty
        };
        var handler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Handle_NotFoundWorkout_ShouldReturnErrorMessage()
    {
        var command = new UpdateWorkoutCommand
        {
            WorkoutId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
            Title = "Updated workout title"
        };
        var handler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ShouldReturnErrorMessage()
    {
        var command = new UpdateWorkoutCommand
        {
            WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d"),
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated workout title"
        };
        var handler = new UpdateWorkoutCommandHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
