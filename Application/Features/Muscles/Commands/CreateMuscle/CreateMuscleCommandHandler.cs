using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Muscles.Commands.CreateMuscle;

public class CreateMuscleCommandHandler : IRequestHandler<CreateMuscleCommand, CreateMuscleCommandResponse>
{
    private readonly IAsyncRepository<Muscle> _muscleRepository;
    private readonly IMapper _mapper;

    public CreateMuscleCommandHandler(IAsyncRepository<Muscle> muscleRepository, IMapper mapper)
    {
        _muscleRepository = muscleRepository;
        _mapper = mapper;
    }

    public async Task<CreateMuscleCommandResponse> Handle(CreateMuscleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateMuscleCommandValidator();
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return new CreateMuscleCommandResponse
            {
                Success = false,
                ValidationErrors = validationResults.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var newMuscle = new Muscle
        {
            Name = request.Name
        };
        newMuscle = await _muscleRepository.AddAsync(newMuscle);

        return new CreateMuscleCommandResponse
        {
            Success = true,
            Muscle = _mapper.Map<CreateMuscleDto>(newMuscle)
        };
    }
}
