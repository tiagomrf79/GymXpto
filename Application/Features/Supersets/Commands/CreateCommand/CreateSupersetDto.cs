namespace Application.Features.Supersets.Commands.CreateCommand;

public class CreateSupersetDto
{
    public Guid SupersetId { get; set; }
    public Guid GroupId { get; set; }
    public int Order { get; set; }
}