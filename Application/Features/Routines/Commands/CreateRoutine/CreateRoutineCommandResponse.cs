using Application.Responses;

namespace Application.Features.Routines.Commands.CreateRoutine;

public class CreateRoutineCommandResponse : BaseResponse
{
	public CreateRoutineDto Routine { get; set; } = default!;
}
