using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Features.Routines.Queries.GetRoutinesList;
using Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;
using Application.Features.Workouts.Commands.CreateWorkout;
using Application.Features.Workouts.Commands.UpdateWorkout;
using Application.Features.Workouts.Queries.GetRoutineWorkoutsList;
using Application.Features.Workouts.Queries.GetWorkoutDetail;
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
        CreateMap<Routine, RoutineDetailDto>();
        CreateMap<Routine, RoutineListVm>();
		CreateMap<Routine, RoutineWorkoutsListVm>();
		CreateMap<Workout, RoutineWorkoutDto>();

		CreateMap<Workout, CreateWorkoutDto>();
		CreateMap<Workout, UpdateWorkoutDto>();
		CreateMap<Workout, UpdateWorkoutCommand>().ReverseMap();
		CreateMap<Workout, WorkoutDetailDto>();
		CreateMap<Workout, WorkoutListVm>();
    }
}
