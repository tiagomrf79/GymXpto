using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Muscles.Commands.UpdateMuscle;

public class UpdateMuscleCommandHandler : IRequestHandler<UpdateMuscleCommand, UpdateMuscleCommandResponse>
{
    private readonly IAsyncRepository<Muscle> _muscleRepository;
    private readonly IMapper _mapper;

    public UpdateMuscleCommandHandler(IAsyncRepository<Muscle> muscleRepository, IMapper mapper)
    {
        _muscleRepository = muscleRepository;
        _mapper = mapper;
    }

    public async Task<UpdateMuscleCommandResponse> Handle(UpdateMuscleCommand request, CancellationToken cancellationToken)
    {
        var muscleToUpdate = await _muscleRepository.GetByIdAsync(request.MuscleId);

        if (muscleToUpdate == null)
        {
            return new UpdateMuscleCommandResponse
            {
                Success = false,
                Message = "Muscle not found."
            };
        }

        var validator = new UpdateMuscleCommandValidator();
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return new UpdateMuscleCommandResponse
            {
                Success = false,
                ValidationErrors = validationResults.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        _mapper.Map(request, muscleToUpdate, typeof(UpdateMuscleCommand), typeof(Muscle));
        await _muscleRepository.UpdateAsync(muscleToUpdate);

        return new UpdateMuscleCommandResponse
        {
            Success = true,
            Muscle = _mapper.Map<UpdateMuscleDto>(muscleToUpdate)
        };
    }
}
