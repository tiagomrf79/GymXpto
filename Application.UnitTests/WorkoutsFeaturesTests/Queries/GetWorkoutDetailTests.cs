using Application.Features.Workouts.Queries.GetWorkoutDetail;
using Application.Interfaces.Persistence;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.WorkoutsFeaturesTests.Queries;

[Collection(nameof(DataCollection))]
public class GetWorkoutDetailTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly IMapper _mapper;

    public GetWorkoutDetailTests(TestFixture testDataFixture)
    {
        _mockWorkoutRepository = testDataFixture.MockWorkoutRepository;
        _mapper = testDataFixture.Mapper;
    }

    [Fact]
    public async Task Handle_ValidWorkout_WorkoutReturnedFromRepo()
    {
        var handler = new GetWorkoutDetailQueryHandler(_mockWorkoutRepository.Object, _mapper);
        var command = new GetWorkoutDetailQuery()
        {
            WorkoutId = new Guid("436a8442-3439-4f11-beac-ffc4269c9950")
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.Workout.WorkoutId.ShouldBe(command.WorkoutId);
    }

    [Fact]
    public async Task Handle_NotFoundWorkout_ErrorMessageReturned()
    {
        var handler = new GetWorkoutDetailQueryHandler(_mockWorkoutRepository.Object, _mapper);
        var command = new GetWorkoutDetailQuery()
        {
            WorkoutId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb")
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
