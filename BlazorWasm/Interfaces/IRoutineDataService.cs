using BlazorWasm.Services.Base;
using BlazorWasm.ViewModels;

namespace BlazorWasm.Interfaces;

public interface IRoutineDataService
{
    Task<ApiResponse<CreateRoutineDto>> CreateRoutine(RoutineViewModel routineViewModel);
    //Task<ApiResponse<UpdateRoutineDto>> UpdateRoutine(RoutineViewModel routineViewModel);
}
