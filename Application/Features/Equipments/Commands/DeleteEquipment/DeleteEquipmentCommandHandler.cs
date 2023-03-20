using Application.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Equipments.Commands.DeleteEquipment;

public class DeleteEquipmentCommandHandler : IRequestHandler<DeleteEquipmentCommand, DeleteEquipmentCommandResponse>
{
    private readonly IAsyncRepository<Equipment> _equipmentRepository;
    private readonly IAsyncRepository<Exercise> _exerciseRepository;

    public DeleteEquipmentCommandHandler(IAsyncRepository<Equipment> equipmentRepository, IAsyncRepository<Exercise> exerciseRepository)
    {
        _equipmentRepository = equipmentRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<DeleteEquipmentCommandResponse> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipmentToDelete = await _equipmentRepository.GetByIdAsync(request.EquipmentId);

        if (equipmentToDelete == null)
        {
            return new DeleteEquipmentCommandResponse
            {
                Success = false,
                Message = "Equipment not found."
            };
        }

        var linkedExercises = (await _exerciseRepository.ListAllAsync()).Where(e => e.MainEquipmentUsedId == request.EquipmentId);

        if (linkedExercises.Count() > 0)
        {
            return new DeleteEquipmentCommandResponse
            {
                Success = false,
                Message = "Equipment cannot be deleted since it's being used."
            };
        }

        await _equipmentRepository.DeleteAsync(equipmentToDelete);

        return new DeleteEquipmentCommandResponse
        {
            Success = true
        };
    }
}
