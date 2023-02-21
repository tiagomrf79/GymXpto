using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Interfaces.Persistence;
using Application.Mappings;
using Application.UnitTests.Mocks;
using AutoMapper;
using Domain.Entities.Schedule;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Commands;

public class UpdateRoutineTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<Routine>> _mockRoutineRepository;

    public UpdateRoutineTests()
    {
        _mockRoutineRepository = RoutineRepositoryMock.GetRoutineRepository();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configurationProvider.CreateMapper(); ;
    }

    [Fact]
    public async Task Handle_ValidRoutine_UpdatedInRoutinesRepo()
    {
        var handler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var command = new UpdateRoutineCommand()
        {
            Id = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c"),
            Title = "Updated routine title",
            Description = "Updated routine description"
        };
        int recordCountBefore = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        int recordCountAfter = (await _mockRoutineRepository.Object.ListAllAsync()).Count;
        recordCountAfter.ShouldBe(recordCountBefore);
        result.Success.ShouldBeTrue();
        result.Routine.Id.ShouldBe(command.Id);
        result.Routine.Title.ShouldBe(command.Title);
        result.Routine.Description.ShouldBe(command.Description);
    }

    [Fact]
    public async Task Handle_InvalidRoutine_ValidationErrorsReturned()
    {
        var handler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var command = new UpdateRoutineCommand()
        {
            Id = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c"),
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
            Id = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated routine title",
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

}
