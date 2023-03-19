using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;

namespace Application.Features.Supersets.Queries.GetSupersetDetail;

public class GetSupersetDetailQueryHandler : IRequestHandler<GetSupersetDetailQuery, GetSupersetDetailQueryResponse>
{
    private readonly IAsyncRepository<Superset> _supersetRepository;
    private readonly IMapper _mapper;

    public GetSupersetDetailQueryHandler(IAsyncRepository<Superset> supersetRepository, IMapper mapper)
    {
        _supersetRepository = supersetRepository;
        _mapper = mapper;
    }

    public async Task<GetSupersetDetailQueryResponse> Handle(GetSupersetDetailQuery request, CancellationToken cancellationToken)
    {
        var supersetFound = await _supersetRepository.GetByIdAsync(request.SupersetId);

        if (supersetFound == null)
        {
            return new GetSupersetDetailQueryResponse
            {
                Success = false,
                Message = "Superset not found."
            };
        }

        return new GetSupersetDetailQueryResponse
        {
            Success = true,
            Superset = _mapper.Map<SupersetDetailDto>(supersetFound)
        };
    }
}
