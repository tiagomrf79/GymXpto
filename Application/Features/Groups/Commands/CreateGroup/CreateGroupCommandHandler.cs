using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, CreateGroupCommandResponse>
{
    private readonly IAsyncRepository<Group> _groupRepository;
    private readonly IAsyncRepository<Workout> _workoutRepository;
    private readonly IMapper _mapper;

    public CreateGroupCommandHandler(IAsyncRepository<Group> groupRepository, IAsyncRepository<Workout> workoutRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _workoutRepository = workoutRepository;
        _mapper = mapper;
    }

    public async Task<CreateGroupCommandResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateGroupCommandValidator(_workoutRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new CreateGroupCommandResponse
            {
                Success = false,
                ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var newGroup = new Group
        {
            WorkoutId = request.WorkoutId,
            Order = request.Order,
            RestBetweenSets = request.RestBetweenSets
        };
        newGroup = await _groupRepository.AddAsync(newGroup);

        return new CreateGroupCommandResponse
        {
            Success = true,
            Group = _mapper.Map<CreateGroupDto>(newGroup)
        };
    }
}
