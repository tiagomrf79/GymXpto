using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.RoutineFeatures.Commands;

public class UpdateRoutineTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;

    public UpdateRoutineTests()
    {
        _mockRoutineRepository = RoutineRepositoryFactory.Create();
        _mapper = MapperFactory.Create();
    }

    [Fact]
    public async Task Handle_ValidRoutine_ShouldPersistInRepository()
    {
        var updateCommand = new UpdateRoutineCommand
        {
            RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
            Title = "Updated routine title",
            Description = "Updated routine description"
        };
        var updateHandler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        await updateHandler.Handle(updateCommand, CancellationToken.None);

        var findQuery = new GetRoutineDetailQuery { RoutineId = updateCommand.RoutineId };
        var findHandler = new GetRoutineDetailQueryHandler(_mockRoutineRepository.Object, _mapper);
        var findResult = await findHandler.Handle(findQuery, CancellationToken.None);

        findResult.Routine.ShouldNotBeNull();
        findResult.Routine.RoutineId.ShouldBe(updateCommand.RoutineId);
        findResult.Routine.Title.ShouldBe(updateCommand.Title);
        findResult.Routine.Description.ShouldBe(updateCommand.Description);
    }

    [Fact]
    public async Task Handle_ValidRoutine_ShouldReturnUpdatedRoutine()
    {
        var command = new UpdateRoutineCommand
        {
            RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
            Title = "Updated routine title",
            Description = "Updated routine description"
        };
        var handler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeTrue();
        result.Routine.ShouldNotBeNull();
        result.Routine.RoutineId.ShouldBe(command.RoutineId);
        result.Routine.Title.ShouldBe(command.Title);
        result.Routine.Description.ShouldBe(command.Description);
    }


    [Fact]
    public async Task Handle_InvalidRoutine_ShouldReturnValidationErrors()
    {
        var command = new UpdateRoutineCommand
        {
            RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
            Title = string.Empty,
        };
        var handler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Handle_NotFoundRoutine_ShouldReturnErrorMessage()
    {
        var command = new UpdateRoutineCommand
        {
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated routine title",
        };
        var handler = new UpdateRoutineCommandHandler(_mockRoutineRepository.Object, _mapper);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
