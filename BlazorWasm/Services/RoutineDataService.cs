using AutoMapper;
using Blazored.LocalStorage;
using BlazorWasm.Interfaces;
using BlazorWasm.Services.Base;
using BlazorWasm.ViewModels;

namespace BlazorWasm.Services;

public class RoutineDataService : BaseDataService, IRoutineDataService
{
    private readonly IMapper _mapper;

    public RoutineDataService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<ApiResponse<CreateRoutineDto>> CreateRoutine(RoutineViewModel routineViewModel)
    {
        try
        {
            var apiResponse = new ApiResponse<CreateRoutineDto>();
            var createRoutineCommand = _mapper.Map<CreateRoutineCommand>(routineViewModel);
            var createRoutineCommandResponse = await _client.AddRoutineAsync(createRoutineCommand);

            if (createRoutineCommandResponse.Success)
            {
                apiResponse.Data = createRoutineCommandResponse.Routine;
                apiResponse.Success = true;
            }
            else
            {
                apiResponse.Data = null;
                foreach (var error in createRoutineCommandResponse.ValidationErrors)
                {
                    apiResponse.ValidationErrors += error + Environment.NewLine;
                }
            }

            return apiResponse;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<CreateRoutineDto>(ex);
        }
    }

}

