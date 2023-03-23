using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Exercises.Commands.UpdateExercise;

public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, UpdateExerciseCommandResponse>
{
    private readonly IAsyncRepository<Exercise> _exerciseRepository;
    private readonly IAsyncRepository<Equipment> _equipmentRepository;
    private readonly IMapper _mapper;

    public UpdateExerciseCommandHandler(IAsyncRepository<Exercise> exerciseRepository, IAsyncRepository<Equipment> equipmentRepository, 
        IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task<UpdateExerciseCommandResponse> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        var exerciseToUpdate = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

        if (exerciseToUpdate == null)
        {
            return new UpdateExerciseCommandResponse
            {
                Success = false,
                Message = "Exercise not found."
            };
        }

        var validator = new UpdateExerciseCommandValidator(_equipmentRepository);
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return new UpdateExerciseCommandResponse
            {
                Success = false,
                ValidationErrors = validationResults.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        _mapper.Map(request, exerciseToUpdate, typeof(UpdateExerciseCommand), typeof(Exercise));
        await _exerciseRepository.UpdateAsync(exerciseToUpdate);

        return new UpdateExerciseCommandResponse
        {
            Success = true,
            Exercise = _mapper.Map<UpdateExerciseDto>(exerciseToUpdate)
        };
    }
}
