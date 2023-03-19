﻿using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.ExerciseSets.Commands.UpdateCommand;

public class UpdateExerciseSetCommandHandler : IRequestHandler<UpdateExerciseSetCommand, UpdateExerciseSetCommandResponse>
{
    private readonly IAsyncRepository<ExerciseSet> _exerciseSetRepository;
    private readonly IAsyncRepository<Superset> _supersetRepository;
    private readonly IAsyncRepository<Exercise> _exerciseRepository;
    private readonly IMapper _mapper;

    public UpdateExerciseSetCommandHandler(IAsyncRepository<ExerciseSet> exerciseSetRepository,
        IAsyncRepository<Superset> supersetRepository, IAsyncRepository<Exercise> exerciseRepository, IMapper mapper)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _supersetRepository = supersetRepository;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<UpdateExerciseSetCommandResponse> Handle(UpdateExerciseSetCommand request, CancellationToken cancellationToken)
    {
        var exerciseSetToUpdate = await _exerciseSetRepository.GetByIdAsync(request.ExerciseSetId);

        if (exerciseSetToUpdate == null)
        {
            return new UpdateExerciseSetCommandResponse
            {
                Success = false,
                Message = "Exercise set not found."
            };
        }

        var validator = new UpdateExerciseSetCommandValidator(_supersetRepository, _exerciseRepository);
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return new UpdateExerciseSetCommandResponse
            {
                Success = false,
                ValidationErrors = validationResults.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        _mapper.Map(request, exerciseSetToUpdate, typeof(UpdateExerciseSetCommand), typeof(ExerciseSet));
        await _exerciseSetRepository.UpdateAsync(exerciseSetToUpdate);

        return new UpdateExerciseSetCommandResponse
        {
            Success = true,
            ExerciseSet = _mapper.Map<UpdateExerciseSetDto>(exerciseSetToUpdate)
        };
    }
}
