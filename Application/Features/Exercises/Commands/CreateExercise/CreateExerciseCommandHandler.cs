using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Exercises.Commands.CreateExercise;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, CreateExerciseCommandResponse>
{
    private readonly IAsyncRepository<Exercise> _exerciseRepository;
    private readonly IAsyncRepository<Muscle> _muscleRepository;
    private readonly IAsyncRepository<Equipment> _equipmentRepository;
    private readonly IMapper _mapper;

    public CreateExerciseCommandHandler(IAsyncRepository<Exercise> exerciseRepository,
        IAsyncRepository<Muscle> muscleRepository, IAsyncRepository<Equipment> equipmentRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _muscleRepository = muscleRepository;
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task<CreateExerciseCommandResponse> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateExerciseCommandValidator(_muscleRepository, _equipmentRepository);
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return new CreateExerciseCommandResponse
            {
                Success = false,
                ValidationErrors = validationResults.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var newExercise = new Exercise
        {
            Name = request.Name,
            Instructions = request.Instructions,
            UtilityType = (UtilityTypes?)request.UtilityType,
            MechanicType = (MechanicTypes)request.MechanicType,
            MovementType = (MovementTypes)request.MovementType,
            MainMuscleWorkedId = request.MainMuscleWorkedId,
            MainEquipmentUsedId = request.MainEquipmentUsedId,
            Comments = request.Comments
        };
        newExercise = await _exerciseRepository.AddAsync(newExercise);

        return new CreateExerciseCommandResponse
        {
            Success = true,
            Exercise = _mapper.Map<CreateExerciseDto>(newExercise)
        };
    }
}
