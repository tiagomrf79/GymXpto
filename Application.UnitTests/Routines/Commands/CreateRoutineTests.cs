using Application.Features.Routines.Commands.CreateRoutine;
using Application.Interfaces.Persistence;
using Application.Mappings;
using Application.UnitTests.Mocks;
using AutoMapper;
using Domain.Entities.Schedule;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Commands;

public class CreateRoutineTests
{
	private readonly IMapper _mapper;
	private readonly Mock<IAsyncRepository<Routine>> _mockRoutineRepository;

	public CreateRoutineTests()
	{
		_mockRoutineRepository = RoutineRepositoryMock.GetRoutineRepository();

		var configurationProvider = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<MappingProfile>();
		});
		_mapper = configurationProvider.CreateMapper();
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
        result.Routine.Title.ShouldBe(command.Title);
        result.Routine.Description.ShouldBe(command.Description);
        result.Success.ShouldBeTrue();
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
