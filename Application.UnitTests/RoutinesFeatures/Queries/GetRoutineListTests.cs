using Application.Features.Routines.Queries.GetRoutinesList;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutinesFeatures.Queries;

public class GetRoutineListTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutineListTests()
    {
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ShouldReturnList()
    {
        var command = new GetRoutinesListQuery();
        var handler = new GetRoutinesListQueryHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.RoutinesList.ShouldNotBeNull();
        result.RoutinesList.Count.ShouldBeGreaterThan(0);
    }
}
