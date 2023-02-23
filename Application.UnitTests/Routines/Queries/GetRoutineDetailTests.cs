using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Interfaces.Persistence;
using Application.Mappings;
using Application.UnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Queries;

public class GetRoutineDetailTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutineDetailTests()
    {
        _mockRoutineRepository = RoutineRepositoryMock.GetRoutineRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_ValidRoutine_RoutineReturnedFromRepo()
    {
        var handler = new GetRoutineDetailQueryHandler(_mockRoutineRepository.Object, _mapper);
        var command = new GetRoutineDetailQuery() { RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98") };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.Routine.RoutineId.ShouldBe(command.RoutineId);
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ErrorMessageReturned()
    {
        var handler = new GetRoutineDetailQueryHandler(_mockRoutineRepository.Object, _mapper);
        var command = new GetRoutineDetailQuery() { RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb") };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
