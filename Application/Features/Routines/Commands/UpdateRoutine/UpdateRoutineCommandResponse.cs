using Application.Responses;

namespace Application.Features.Routines.Commands.UpdateRoutine;

public class UpdateRoutineCommandResponse : BaseResponse
{
	public UpdateRoutineDto Routine { get; set; } = default!;

	public UpdateRoutineCommandResponse() : base()
	{
	}
}
