using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Equipments.Commands.CreateEquipment;

public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, CreateEquipmentCommandResponse>
{
    private readonly IAsyncRepository<Equipment> _equipmentRepository;
    private readonly IMapper _mapper;

    public CreateEquipmentCommandHandler(IAsyncRepository<Equipment> equipmentRepository, IMapper mapper)
    {
        _equipmentRepository = equipmentRepository;
        _mapper = mapper;
    }

    public async Task<CreateEquipmentCommandResponse> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateEquipmentCommandValidator();
        var validationResults = await validator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return new CreateEquipmentCommandResponse
            {
                Success = false,
                ValidationErrors = validationResults.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var newEquipment = new Equipment
        {
            Name = request.Name
        };
        newEquipment = await _equipmentRepository.AddAsync(newEquipment);

        return new CreateEquipmentCommandResponse
        {
            Success = true,
            Equipment = _mapper.Map<CreateEquipmentDto>(newEquipment)
        };
    }
}
