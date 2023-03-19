using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Supersets.Commands.CreateCommand;

public class CreateSupersetCommandHandler : IRequestHandler<CreateSupersetCommand, CreateSupersetCommandResponse>
{
    private readonly IAsyncRepository<Superset> _supersetRepository;
    private readonly IAsyncRepository<Group> _groupRepository;
    private readonly IMapper _mapper;

    public CreateSupersetCommandHandler(IAsyncRepository<Superset> supersetRepository, IAsyncRepository<Group> groupRepository, IMapper mapper)
    {
        _supersetRepository = supersetRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<CreateSupersetCommandResponse> Handle(CreateSupersetCommand request, CancellationToken cancellationToken)
    {
        var groupFound = await _groupRepository.GetByIdAsync(request.GroupId);

        if (groupFound == null)
        {
            return new CreateSupersetCommandResponse
            {
                Success = false,
                Message = "Group not found."
            };
        }

        var validator = new CreateSupersetCommandValidator();
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
