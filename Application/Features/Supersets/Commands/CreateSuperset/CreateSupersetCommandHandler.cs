using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Supersets.Commands.CreateSuperset;

public class CreateSupersetCommandHandler : IRequestHandler<CreateSupersetCommand, CreateSupersetCommandResponse>
{
    private readonly ISupersetRepository _supersetRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public CreateSupersetCommandHandler(ISupersetRepository supersetRepository, IGroupRepository groupRepository, IMapper mapper)
    {
        _supersetRepository = supersetRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<CreateSupersetCommandResponse> Handle(CreateSupersetCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSupersetCommandValidator(_groupRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new CreateSupersetCommandResponse
            {
                Success = false,
                ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        }

        var newSuperset = new Superset
        {
            GroupId = request.GroupId,
            Order = request.Order
        };
        newSuperset = await _supersetRepository.AddAsync(newSuperset);

        return new CreateSupersetCommandResponse
        {
            Success = true,
            Superset = _mapper.Map<CreateSupersetDto>(newSuperset)
        };
    }
}
