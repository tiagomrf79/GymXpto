using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Routines.Commands.CreateRoutine;

public class CreateRoutineCommandHandler : IRequestHandler<CreateRoutineCommand, CreateRoutineCommandResponse>
{
    private readonly IAsyncRepository<Routine> _routineRepository;
    private readonly IMapper _mapper;

    public CreateRoutineCommandHandler(IAsyncRepository<Routine> routineRepository, IMapper mapper)
    {
        _routineRepository = routineRepository;
        _mapper = mapper;
    }

    public async Task<CreateRoutineCommandResponse> Handle(CreateRoutineCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateRoutineCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new CreateRoutineCommandResponse
            {
                Success = false,
                ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var newRoutine = new Routine() {
            Title = request.Title,
            Description = request.Description
        };
        newRoutine = await _routineRepository.AddAsync(newRoutine);

        return new CreateRoutineCommandResponse
        {
            Success = true,
            Routine = _mapper.Map<CreateRoutineDto>(newRoutine)
        };
    }
}
