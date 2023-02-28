using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Interfaces.Persistence;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutinesFeaturesTests.Queries;

[Collection(nameof(DataCollection))]
public class GetRoutineDetailTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutineDetailTests(TestFixture testDataFixture)
    {
        _mockRoutineRepository = testDataFixture.MockRoutineRepository;
        _mapper = testDataFixture.Mapper;
    }

    [Fact]
    public async Task Handle_ValidRoutine_RoutineReturnedFromRepo()
    {
        var handler = new GetRoutineDetailQueryHandler(_mockRoutineRepository.Object, _mapper);
        var command = new GetRoutineDetailQuery()
        {
            RoutineId = new Guid("da572ec2-0f0b-4094-bfa7-f51329df41c6")
        };

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
