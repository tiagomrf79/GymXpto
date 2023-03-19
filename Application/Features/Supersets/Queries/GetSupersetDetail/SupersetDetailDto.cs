namespace Application.Features.Supersets.Queries.GetSupersetDetail;

public class SupersetDetailDto
{
    public Guid SupersetId { get; set; }
    public Guid GroupId { get; set; }
    public int Order { get; set; }
}