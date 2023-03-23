using Application.Features.Routines.Queries.GetRoutineList;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutineFeatures.Queries;

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
        var command = new GetRoutineListQuery();
        var handler = new GetRoutineListQueryHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.RoutineList.ShouldNotBeNull();
        result.RoutineList.ShouldNotBeEmpty();
    }
}
