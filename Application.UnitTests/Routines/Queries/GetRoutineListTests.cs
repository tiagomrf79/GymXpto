using Application.Features.Routines.Queries.GetRoutinesList;
using Application.Interfaces.Persistence;
using Application.Mappings;
using Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Queries;

public class GetRoutineListTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutineListTests()
    {
        _mockRoutineRepository = RoutineRepositoryMock.GetRoutineRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_RoutineListReturned()
    {
        var handler = new GetRoutinesListQueryHandler(_mockRoutineRepository.Object, _mapper);
        var command = new GetRoutinesListQuery();
        int recordCount = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        result.ShouldBeOfType<List<RoutineListVm>>();
        result.Count.ShouldBe(recordCount);
    }
}
