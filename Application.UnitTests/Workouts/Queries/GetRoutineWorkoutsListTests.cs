using Application.Features.Workouts.Queries.GetRoutineWorkoutsList;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Workouts.Queries;

[Collection(nameof(DataCollection))]
public class GetRoutineWorkoutsListTests
{
    private readonly Mock<IWorkoutRepository> _mockWorkoutRepository;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutineWorkoutsListTests(TestFixture testDataFixture)
    {
        _mockWorkoutRepository = testDataFixture.MockWorkoutRepository;
        _mockRoutineRepository = testDataFixture.MockRoutineRepository;
        _mapper = testDataFixture.Mapper;
    }

    [Fact]
    public async Task Handle_WorkoutsListReturned()
    {
        var handler = new GetRoutineWorkoutsListQueryHandler(_mockWorkoutRepository.Object, _mockRoutineRepository.Object, _mapper);
        var command = new GetRoutineWorkoutsListQuery()
        {
            RoutineId = new Guid("da572ec2-0f0b-4094-bfa7-f51329df41c6")
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.WorkoutsList.ShouldNotBeNull();
        result.WorkoutsList.Count.ShouldBeGreaterThan(0);
    }
}
