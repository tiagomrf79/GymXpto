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
        var createRoutineCommandResponse = new CreateRoutineCommandResponse();

        var validator = new CreateRoutineCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            createRoutineCommandResponse.Success = false;
            createRoutineCommandResponse.ValidationErrors = new List<string>();

            foreach (var error in validationResult.Errors)
            {
                createRoutineCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (createRoutineCommandResponse.Success)
        {
            var routine = new Routine() {
                //TODO: add UserId
                //UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            routine = await _routineRepository.AddAsync(routine);
            createRoutineCommandResponse.Routine = _mapper.Map<CreateRoutineDto>(routine);
        }

        return createRoutineCommandResponse;
    }
}
