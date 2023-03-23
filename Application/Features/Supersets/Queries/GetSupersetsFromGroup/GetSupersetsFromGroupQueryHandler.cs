using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Supersets.Queries.GetSupersetsFromGroup;

public class GetSupersetsFromGroupQueryHandler : IRequestHandler<GetSupersetsFromGroupQuery, GetSupersetsFromGroupQueryResponse>
{
    private readonly ISupersetRepository _supersetRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GetSupersetsFromGroupQueryHandler(ISupersetRepository supersetRepository, IGroupRepository groupRepository, IMapper mapper)
    {
        _supersetRepository = supersetRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<GetSupersetsFromGroupQueryResponse> Handle(GetSupersetsFromGroupQuery request, CancellationToken cancellationToken)
    {
        var groupFound = await _groupRepository.GetByIdAsync(request.GroupId);

        if (groupFound == null)
        {
            return new GetSupersetsFromGroupQueryResponse
            {
                Success = false,
                Message = "Group not found."
            };
        }

        var supersetList = await _supersetRepository.GetSupersetsFromGroup(request.GroupId);

        return new GetSupersetsFromGroupQueryResponse
        {
            Success = true,
            SupersetsList = _mapper.Map<List<SupersetListVm>>(supersetList)
        };
    }
}
