﻿using Application.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Groups.Queries.GetGroupDetail;

public class GetGroupDetailQueryHandler : IRequestHandler<GetGroupDetailQuery, GetGroupDetailQueryResponse>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GetGroupDetailQueryHandler(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<GetGroupDetailQueryResponse> Handle(GetGroupDetailQuery request, CancellationToken cancellationToken)
    {
        var groupFound = await _groupRepository.GetByIdAsync(request.GroupId);

        if (groupFound == null)
        {
            return new GetGroupDetailQueryResponse
            {
                Success = false,
                Message = "Group not found."
            };
        }

        return new GetGroupDetailQueryResponse
        {
            Success = true,
            Group = _mapper.Map<GroupDetailDto>(groupFound)
        };
    }
}
