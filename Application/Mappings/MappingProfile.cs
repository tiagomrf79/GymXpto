using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using AutoMapper;
using Domain.Entities.Schedule;

namespace Application.Mappings;

public class MappingProfile : Profile
{
	//TODO: write mappings between entities and view models
	public MappingProfile()
	{
		CreateMap<Routine, CreateRoutineDto>();
		CreateMap<Routine, UpdateRoutineDto>();
        CreateMap<Routine, UpdateRoutineCommand>().ReverseMap();
    }
}
