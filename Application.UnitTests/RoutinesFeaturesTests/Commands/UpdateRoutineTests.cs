using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Interfaces.Persistence;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutinesFeaturesTests.Commands;

[Collection(nameof(DataCollection))]
public class UpdateRoutineTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;

    public UpdateRoutineTests(TestFixture testDataFixture)
    {
        _mockRoutineRepository = testDataFixture.MockRoutineRepository;
        _mapper = testDataFixture.Mapper;
    }

    [Fact]
    public async Task Handle_ValidRoutine_UpdatedInRoutinesRepo()
    {
        var handler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var command = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
            Title = "Updated routine title",
            Description = "Updated routine description"
        };
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeTrue();
        result.Routine.RoutineId.ShouldBe(command.RoutineId);
        result.Routine.Title.ShouldBe(command.Title);
        result.Routine.Description.ShouldBe(command.Description);
    }

    [Fact]
    public async Task Handle_InvalidRoutine_ValidationErrorsReturned()
    {
        var handler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var command = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
            Title = string.Empty,
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ErrorMessageReturned()
    {
        var handler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var command = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated routine title",
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

}
