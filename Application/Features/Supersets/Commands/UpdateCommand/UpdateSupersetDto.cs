﻿namespace Application.Features.Supersets.Commands.UpdateCommand;

public class UpdateSupersetDto
{
    public Guid SupersetId { get; set; }
    public Guid GroupId { get; set; }
    public int Order { get; set; }
}