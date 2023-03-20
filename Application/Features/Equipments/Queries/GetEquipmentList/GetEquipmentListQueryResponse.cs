using Application.Responses;

namespace Application.Features.Equipments.Queries.GetEquipmentList;

public class GetEquipmentListQueryResponse : BaseResponse
{
    public IList<EquipmentListVm> EquipmentList { get; set; } = new List<EquipmentListVm>();
}