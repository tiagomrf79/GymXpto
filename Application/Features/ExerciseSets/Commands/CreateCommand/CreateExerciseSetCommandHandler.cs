using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.ExerciseSets.Commands.CreateCommand;

public class CreateExerciseSetCommandHandler : IRequestHandler<CreateExerciseSetCommand, CreateExerciseSetCommandResponse>
{
    private readonly IAsyncRepository<ExerciseSet> _exerciseSetRepository;
    private readonly IAsyncRepository<Superset> _supersetRepository;
    private readonly IAsyncRepository<Exercise> _exerciseRepository;
    private readonly IMapper _mapper;

    public CreateExerciseSetCommandHandler(IAsyncRepository<ExerciseSet> exerciseSetRepository,
        IAsyncRepository<Superset> supersetRepository, IAsyncRepository<Exercise> exerciseRepository, IMapper mapper)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _supersetRepository = supersetRepository;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<CreateExerciseSetCommandResponse> Handle(CreateExerciseSetCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateExerciseSetCommandValidator(_supersetRepository, _exerciseRepository);
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return new CreateExerciseSetCommandResponse
            {
                Success = false,
                ValidationErrors = validationResults.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var newExerciseSet = new ExerciseSet
        {
            SupersetId = request.SupersetId,
            Order = request.Order,
            TargetRepetitions = request.TargetRepetitions,
            ExerciseId = request.ExerciseId
        };
        newExerciseSet = await _exerciseSetRepository.AddAsync(newExerciseSet);

        return new CreateExerciseSetCommandResponse
        {
            Success = true,
            ExerciseSet = _mapper.Map<CreateExerciseSetDto>(newExerciseSet)
        };
    }
}
