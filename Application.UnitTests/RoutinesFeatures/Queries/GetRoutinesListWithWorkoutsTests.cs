using Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutinesFeatures.Queries;

public class GetRoutinesListWithWorkoutsTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutinesListWithWorkoutsTests()
    {
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ShouldReturnListIncludingWorkouts()
    {
        var command = new GetRoutinesListWithWorkoutsQuery();
        var handler = new GetRoutinesListWithWorkoutsQueryHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.ShouldBeOfType<List<RoutineWorkoutsListVm>>();
        result.Count.ShouldBeGreaterThan(0);
        result.ForEach(r => r.Workouts.ShouldNotBeNull());
    }
}
