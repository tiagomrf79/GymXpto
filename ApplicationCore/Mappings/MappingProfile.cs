using Application.Features.Routines.Commands.CreateRoutine;
using AutoMapper;
using Domain.Entities.Schedule;

namespace Application.Mappings;

public class MappingProfile : Profile
{
	//TODO: write mappings between entities and view models
	public MappingProfile()
	{
		CreateMap<Routine, CreateRoutineDto>();
	}
}
