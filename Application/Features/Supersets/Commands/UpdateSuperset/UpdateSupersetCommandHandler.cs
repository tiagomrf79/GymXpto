﻿using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Supersets.Commands.UpdateSuperset;

public class UpdateSupersetCommandHandler : IRequestHandler<UpdateSupersetCommand, UpdateSupersetCommandResponse>
{
    private readonly ISupersetRepository _supersetRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public UpdateSupersetCommandHandler(ISupersetRepository supersetRepository, IGroupRepository groupRepository, IMapper mapper)
    {
        _supersetRepository = supersetRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<UpdateSupersetCommandResponse> Handle(UpdateSupersetCommand request, CancellationToken cancellationToken)
    {
        var supersetToUpdate = await _supersetRepository.GetByIdAsync(request.SupersetId);

        if (supersetToUpdate == null)
        {
            return new UpdateSupersetCommandResponse
            {
                Success = false,
                Message = "Superset not found."
            };
        }

        var validator = new UpdateSupersetCommandValidator(_groupRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new UpdateSupersetCommandResponse
            {
                Success = false,
                ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        _mapper.Map(request, supersetToUpdate, typeof(UpdateSupersetCommand), typeof(Superset));
        await _supersetRepository.UpdateAsync(supersetToUpdate);

        return new UpdateSupersetCommandResponse
        {
            Success = true,
            Superset = _mapper.Map<UpdateSupersetDto>(supersetToUpdate)
        };
    }
}
