using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Exercises.Queries.GetExerciseDetail;

public class GetExerciseDetailQueryHandler : IRequestHandler<GetExerciseDetailQuery, GetExerciseDetailQueryResponse>
{
    private readonly IAsyncRepository<Exercise> _exerciseRepository;
    private readonly IMapper _mapper;

    public GetExerciseDetailQueryHandler(IAsyncRepository<Exercise> exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<GetExerciseDetailQueryResponse> Handle(GetExerciseDetailQuery request, CancellationToken cancellationToken)
    {
        var exerciseFound = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

        if (exerciseFound == null)
        {
            return new GetExerciseDetailQueryResponse
            {
                Success = false,
                Message = "Exercise not found."
            };
        }

        return new GetExerciseDetailQueryResponse
        {
            Success = true,
            Exercise = _mapper.Map<ExerciseDetailDto>(exerciseFound)
        };
    }
}
