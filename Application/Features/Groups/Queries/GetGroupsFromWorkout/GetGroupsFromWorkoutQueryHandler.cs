using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Groups.Queries.GetGroupsFromWorkout;

public class GetGroupsFromWorkoutQueryHandler : IRequestHandler<GetGroupsFromWorkoutQuery, GetGroupsFromWorkoutQueryResponse>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IMapper _mapper;

    public GetGroupsFromWorkoutQueryHandler(IGroupRepository groupRepository, IWorkoutRepository workoutRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _workoutRepository = workoutRepository;
        _mapper = mapper;
    }

    public async Task<GetGroupsFromWorkoutQueryResponse> Handle(GetGroupsFromWorkoutQuery request, CancellationToken cancellationToken)
    {
        var workoutFound = await _workoutRepository.GetByIdAsync(request.WorkoutId);

        if (workoutFound == null)
        {
            return new GetGroupsFromWorkoutQueryResponse
            {
                Success = false,
                Message = "Workout not found."
            };
        }

        var groupList = await _groupRepository.GetGroupsFromWorkout(request.WorkoutId);

        return new GetGroupsFromWorkoutQueryResponse
        {
            Success = true,
            GroupsList = _mapper.Map<List<GroupListVm>>(groupList)
        };
    }
}
