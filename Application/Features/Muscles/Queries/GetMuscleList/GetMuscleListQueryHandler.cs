using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Muscles.Queries.GetMuscleList;

public class GetMuscleListQueryHandler : IRequestHandler<GetMuscleListQuery, GetMuscleListQueryResponse>
{
    private readonly IAsyncRepository<Muscle> _muscleRepository;
    private readonly IMapper _mapper;

    public GetMuscleListQueryHandler(IAsyncRepository<Muscle> muscleRepository, IMapper mapper)
    {
        _muscleRepository = muscleRepository;
        _mapper = mapper;
    }

    public async Task<GetMuscleListQueryResponse> Handle(GetMuscleListQuery request, CancellationToken cancellationToken)
    {
        var muscleList = (await _muscleRepository.ListAllAsync()).OrderBy(m => m.Name);

        return new GetMuscleListQueryResponse
        {
            Success = true,
            Muscle = _mapper.Map<List<MuscleListVm>>(muscleList)
        };
    }
}
