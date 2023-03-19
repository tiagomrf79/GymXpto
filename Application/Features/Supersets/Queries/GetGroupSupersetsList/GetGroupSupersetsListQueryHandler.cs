using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Supersets.Queries.GetGroupSupersetsList;

public class GetGroupSupersetsListQueryHandler : IRequestHandler<GetGroupSupersetsListQuery, GetGroupSupersetsListQueryResponse>
{
    private readonly IAsyncRepository<Superset> _supersetRepository;
    private readonly IAsyncRepository<Group> _groupRepository;
    private readonly IMapper _mapper;

    public GetGroupSupersetsListQueryHandler(IAsyncRepository<Superset> supersetRepository, IAsyncRepository<Group> groupRepository, IMapper mapper)
    {
        _supersetRepository = supersetRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<GetGroupSupersetsListQueryResponse> Handle(GetGroupSupersetsListQuery request, CancellationToken cancellationToken)
    {
        var groupFound = await _groupRepository.GetByIdAsync(request.GroupId);

        if (groupFound == null)
        {
            return new GetGroupSupersetsListQueryResponse
            {
                Success = false,
                Message = "Group not found."
            };
        }

        var supersetsList = (await _supersetRepository.ListAllAsync()).Where(s => s.GroupId == request.GroupId);

        return new GetGroupSupersetsListQueryResponse
        {
            Success = true,
            SupersetsList = _mapper.Map<List<SupersetListVm>>(supersetsList)
        };
    }
}
