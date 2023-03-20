﻿using Application.Features.Equipments.Commands.CreateEquipment;
using Application.Features.Equipments.Commands.UpdateEquipment;
using Application.Features.Equipments.Queries.GetEquipmentList;
using Application.Features.Exercises.Commands.CreateExercise;
using Application.Features.Exercises.Commands.UpdateExercise;
using Application.Features.Exercises.Queries.GetExerciseDetail;
using Application.Features.Exercises.Queries.GetExerciseList;
using Application.Features.ExerciseSets.Commands.CreateExerciseSet;
using Application.Features.ExerciseSets.Commands.UpdateExerciseSet;
using Application.Features.ExerciseSets.Queries.GetExerciseSetDetail;
using Application.Features.ExerciseSets.Queries.GetSupersetExerciseSetsList;
using Application.Features.Groups.Commands.CreateGroup;
using Application.Features.Groups.Commands.UpdateGroup;
using Application.Features.Groups.Queries.GetGroupDetail;
using Application.Features.Groups.Queries.GetWorkoutGroupsList;
using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Features.Routines.Queries.GetRoutinesList;
using Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;
using Application.Features.Supersets.Commands.CreateSuperset;
using Application.Features.Supersets.Commands.UpdateSuperset;
using Application.Features.Supersets.Queries.GetGroupSupersetsList;
using Application.Features.Supersets.Queries.GetSupersetDetail;
using Application.Features.Workouts.Commands.CreateWorkout;
using Application.Features.Workouts.Commands.UpdateWorkout;
using Application.Features.Workouts.Queries.GetRoutineWorkoutsList;
using Application.Features.Workouts.Queries.GetWorkoutDetail;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Schedule;

namespace Application.Mappings;

public class MappingProfile : Profile
{
	//TODO: write mappings between entities and view models
	public MappingProfile()
	{
		CreateMap<Routine, CreateRoutineDto>();
        CreateMap<Routine, UpdateRoutineCommand>().ReverseMap();
        CreateMap<Routine, UpdateRoutineDto>();
        CreateMap<Routine, RoutineDetailDto>();
        CreateMap<Routine, RoutineListVm>();
		CreateMap<Routine, RoutineWorkoutsListVm>();
		CreateMap<Workout, RoutineWorkoutDto>();

		CreateMap<Workout, CreateWorkoutDto>();
        CreateMap<Workout, UpdateWorkoutCommand>().ReverseMap();
        CreateMap<Workout, UpdateWorkoutDto>();
		CreateMap<Workout, WorkoutDetailDto>();
		CreateMap<Workout, WorkoutListVm>();

		CreateMap<Group, CreateGroupDto>();
		CreateMap<Group, UpdateRoutineCommand>().ReverseMap();
		CreateMap<Group, UpdateGroupDto>();
		CreateMap<Group, GroupDetailDto>();
		CreateMap<Group, GroupListVm>();

		CreateMap<Superset, CreateSupersetDto>();
		CreateMap<Superset, UpdateSupersetCommand>().ReverseMap();
		CreateMap<Superset, UpdateSupersetDto>();
		CreateMap<Superset, SupersetDetailDto>();
		CreateMap<Superset, SupersetListVm>();

		CreateMap<ExerciseSet, CreateExerciseSetDto>();
		CreateMap<ExerciseSet, UpdateExerciseSetCommand>().ReverseMap();
		CreateMap<ExerciseSet, UpdateExerciseSetDto>();
		CreateMap<ExerciseSet, ExerciseSetDetailDto>();
		CreateMap<ExerciseSet, ExerciseSetListVm>();

		CreateMap<Exercise, CreateExerciseDto>();
		CreateMap<Exercise, UpdateExerciseCommand>().ReverseMap();
		CreateMap<Exercise, UpdateExerciseDto>();
		CreateMap<Exercise, ExerciseDetailDto>();
		CreateMap<Exercise, ExerciseListVm>();

        CreateMap<Equipment, CreateEquipmentDto>();
        CreateMap<Equipment, UpdateEquipmentCommand>().ReverseMap();
        CreateMap<Equipment, UpdateEquipmentDto>();
        CreateMap<Equipment, EquipmentListVm>();
    }
}
