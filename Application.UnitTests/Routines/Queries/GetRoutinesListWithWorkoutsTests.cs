using Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;
using Application.Interfaces.Persistence;
using Application.Mappings;
using Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Queries;

public class GetRoutinesListWithWorkoutsTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutinesListWithWorkoutsTests()
    {
        _mockRoutineRepository = RoutineRepositoryMock.GetRoutineRepository();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_RoutinesListWithWorkoutsReturned()
    {
        var handler = new GetRoutinesListWithWorkoutsQueryHandler(_mockRoutineRepository.Object, _mapper);
        var command = new GetRoutinesListWithWorkoutsQuery();
        int routineCount = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        result.ShouldBeOfType<List<RoutineWorkoutsListVm>>();
        result.Count.ShouldBe(routineCount);
        result.ForEach(r => r.Workouts.ShouldNotBeNull());
    }
}
