using Application.Features.Routines.Commands.CreateRoutine;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutinesFeatures.Commands;

public class CreateRoutineTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;

    public CreateRoutineTests()
    {
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ValidRoutine_ShouldPersistInRepository()
    {
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var command = new CreateRoutineCommand
        {
            Title = "New routine title",
            Description = "New routine description"
        };
        var handler = new CreateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        recordCountAfter.ShouldBe(recordCountBefore + 1);
    }

    [Fact]
    public async Task Handle_ValidRoutine_ShouldReturnAddedRoutine()
    {
        var command = new CreateRoutineCommand
        {
            Title = "New routine title",
            Description = "New routine description"
        };
        var handler = new CreateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.Routine.ShouldNotBeNull();
        result.Routine.Title.ShouldBe(command.Title);
        result.Routine.Description.ShouldBe(command.Description);
    }

    [Fact]
    public async Task Handle_InvalidRoutine_ShouldReturnValidationErrors()
    {
        var command = new CreateRoutineCommand { Title = "" };
        var handler = new CreateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }
}
