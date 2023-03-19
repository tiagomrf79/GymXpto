using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Groups.Queries.GetWorkoutGroupsList;

public class GetWorkoutGroupsListQueryHandler : IRequestHandler<GetWorkoutGroupsListQuery, GetWorkoutGroupsListQueryResponse>
{
    private readonly IAsyncRepository<Group> _groupRepository;
    private readonly IAsyncRepository<Workout> _workoutRepository;
    private readonly IMapper _mapper;

    public GetWorkoutGroupsListQueryHandler(IAsyncRepository<Group> groupRepository, IAsyncRepository<Workout> workoutRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _workoutRepository = workoutRepository;
        _mapper = mapper;
    }

    public async Task<GetWorkoutGroupsListQueryResponse> Handle(GetWorkoutGroupsListQuery request, CancellationToken cancellationToken)
    {
        var workoutFound = await _workoutRepository.GetByIdAsync(request.WorkoutId);

        if (workoutFound == null)
        {
            return new GetWorkoutGroupsListQueryResponse
            {
                Success = false,
                Message = "Workout not found."
            };
        }

        var groupsList = (await _groupRepository.ListAllAsync()).Where(g => g.WorkoutId == request.WorkoutId);

        return new GetWorkoutGroupsListQueryResponse
        {
            Success = true,
            GroupsList = _mapper.Map<List<GroupListVm>>(groupsList)
        };
    }
}
