using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using MediatR;
using System.Runtime.CompilerServices;

namespace Application.Features.Routines.Commands.DeleteRoutine;

internal class DeleteRoutineCommandHandler : IRequestHandler<DeleteRoutineCommand>
{
    private readonly IAsyncRepository<Routine> _asyncRepository;
    private readonly IMapper _mapper;

    public DeleteRoutineCommandHandler(IAsyncRepository<Routine> asyncRepository, IMapper mapper)
    {
        _asyncRepository = asyncRepository;
        _mapper = mapper;
    }
    public Task<Unit> Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
