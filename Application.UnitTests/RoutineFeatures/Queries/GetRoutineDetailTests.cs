using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutineFeatures.Queries;

public class GetRoutineDetailTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutineDetailTests()
    {
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ValidRoutine_ShouldReturnRoutine()
    {
        var command = new GetRoutineDetailQuery { RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab") };
        var handler = new GetRoutineDetailQueryHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.Routine.ShouldNotBeNull();
        result.Routine.RoutineId.ShouldBe(command.RoutineId);
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ShouldReturnErrorMessage()
    {
        var command = new GetRoutineDetailQuery() { RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb") };
        var handler = new GetRoutineDetailQueryHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
