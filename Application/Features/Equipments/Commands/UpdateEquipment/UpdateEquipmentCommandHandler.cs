using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Equipments.Commands.UpdateEquipment;

public class UpdateEquipmentCommandHandler : IRequestHandler<UpdateEquipmentCommand, UpdateEquipmentCommandResponse>
{
    private readonly IAsyncRepository<Equipment> _equipmentRepository;
    private readonly IMapper _mapper;

    public UpdateEquipmentCommandHandler(IAsyncRepository<Equipment> equipmentRepository, IMapper mapper)
    {
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task<UpdateEquipmentCommandResponse> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipmentToUpdate = await _equipmentRepository.GetByIdAsync(request.EquipmentId);

        if (equipmentToUpdate == null)
        {
            return new UpdateEquipmentCommandResponse
            {
                Success = false,
                Message = "Equipment not found."
            };
        }

        var validator = new UpdateEquipmentCommandValidator();
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return new UpdateEquipmentCommandResponse
            {
                Success = false,
                ValidationErrors = validationResults.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        _mapper.Map(request, equipmentToUpdate, typeof(UpdateEquipmentCommand), typeof(Equipment));
        await _equipmentRepository.UpdateAsync(equipmentToUpdate);

        return new UpdateEquipmentCommandResponse
        {
            Success = true,
            Equipment = _mapper.Map<UpdateEquipmentDto>(equipmentToUpdate)
        };
    }
}
