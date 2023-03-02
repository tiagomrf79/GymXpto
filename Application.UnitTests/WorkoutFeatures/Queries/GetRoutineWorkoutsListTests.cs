using Application.Features.Workouts.Queries.GetRoutineWorkoutsList;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.WorkoutFeatures.Queries;

public class GetRoutineWorkoutsListTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutineWorkoutsListTests()
    {
        _mockWorkoutRepository = WorkoutRepositoryFactory.Create();
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ShouldReturnList()
    {
        var command = new GetRoutineWorkoutsListQuery { RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad") };
        var handler = new GetRoutineWorkoutsListQueryHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.WorkoutsList.ShouldNotBeNull();
        result.WorkoutsList.Count.ShouldBeGreaterThan(0);
    }
}
