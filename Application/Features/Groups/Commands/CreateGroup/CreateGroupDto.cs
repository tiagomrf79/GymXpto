﻿namespace Application.Features.Groups.Commands.CreateGroup;

public class CreateGroupDto
{
    public Guid GroupId { get; set; }
    public Guid WorkoutId { get; set; }
    public int Order { get; set; }
    public int RestBetweenSets { get; set; }
}