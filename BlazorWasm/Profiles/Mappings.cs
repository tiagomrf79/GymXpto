using AutoMapper;
using BlazorWasm.Services.Base;
using BlazorWasm.ViewModels;

namespace BlazorWasm.Profiles;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<CreateWorkoutCommand, RoutineViewModel>().ReverseMap();
    }
}
