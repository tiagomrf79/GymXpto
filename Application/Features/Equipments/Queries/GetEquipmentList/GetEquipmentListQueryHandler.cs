using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Equipments.Queries.GetEquipmentList;

public class GetEquipmentListQueryHandler : IRequestHandler<GetEquipmentListQuery, GetEquipmentListQueryResponse>
{
    private readonly IAsyncRepository<Equipment> _equipmentRepository;
    private readonly IMapper _mapper;

    public GetEquipmentListQueryHandler(IAsyncRepository<Equipment> equipmentRepository, IMapper mapper)
    {
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task<GetEquipmentListQueryResponse> Handle(GetEquipmentListQuery request, CancellationToken cancellationToken)
    {
        var equipmentList = (await _equipmentRepository.ListAllAsync()).OrderBy(e => e.Name);

        return new GetEquipmentListQueryResponse
        {
            Success = true,
            EquipmentList = _mapper.Map<List<EquipmentListVm>>(equipmentList)
        };
    }
}
