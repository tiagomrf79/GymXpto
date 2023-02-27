using Application.Features.Routines.Commands.CreateRoutine;
using Application.Interfaces.Persistence;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Commands;

[Collection(nameof(DataCollection))]
public class CreateRoutineTests
{
    private readonly IMapper _mapper;
	private readonly Mock<IRoutineRepository> _mockRoutineRepository;

	public CreateRoutineTests(TestFixture testDataFixture)
	{
        _mockRoutineRepository = testDataFixture.MockRoutineRepository;
		_mapper = testDataFixture.Mapper;
	}

	[Fact]
	public async Task Handle_ValidRoutine_AddedToRoutinesRepo()
	{
        var handler = new CreateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var command = new CreateRoutineCommand()
        {
            Title = "New routine title",
            Description = "New routine description"
        };
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore + 1);
        result.Success.ShouldBeTrue();
        result.Routine.Title.ShouldBe(command.Title);
        result.Routine.Description.ShouldBe(command.Description);
    }

    [Fact]
    public async Task Handle_InvalidRoutine_ValidationErrorsReturned()
    {
        var handler = new CreateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(new CreateRoutineCommand() { Title = "" }, CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

}
