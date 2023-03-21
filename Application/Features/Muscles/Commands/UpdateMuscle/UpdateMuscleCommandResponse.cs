using Application.Responses;

namespace Application.Features.Muscles.Commands.UpdateMuscle;

public class UpdateMuscleCommandResponse : BaseResponse
{
    public UpdateMuscleDto Muscle { get; set; } = default!;
}