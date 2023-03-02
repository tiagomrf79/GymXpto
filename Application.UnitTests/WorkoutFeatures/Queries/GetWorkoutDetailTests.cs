using Application.Features.Workouts.Queries.GetWorkoutDetail;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.WorkoutFeatures.Queries;

public class GetWorkoutDetailTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly IMapper _mapper;

    public GetWorkoutDetailTests()
    {
        _mockWorkoutRepository = WorkoutRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ValidWorkout_ShouldReturnWorkout()
    {
        var command = new GetWorkoutDetailQuery { WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a") };
        var handler = new GetWorkoutDetailQueryHandler(_mockWorkoutRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.Workout.ShouldNotBeNull();
        result.Workout.WorkoutId.ShouldBe(command.WorkoutId);
    }

    [Fact]
    public async Task Handle_NotFoundWorkout_ErrorMessageReturned()
    {
        var command = new GetWorkoutDetailQuery { WorkoutId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb") };
        var handler = new GetWorkoutDetailQueryHandler(_mockWorkoutRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
