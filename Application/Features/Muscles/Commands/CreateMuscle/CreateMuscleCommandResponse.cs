using Application.Features.Muscles.Commands.CreateMuscle;
using Application.Responses;

public class CreateMuscleCommandResponse : BaseResponse
{
    public CreateMuscleDto Muscle { get; set; } = default!;
}