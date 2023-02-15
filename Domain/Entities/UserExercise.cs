using Domain.Common;

namespace Domain.Entities;

public class UserExercise : Exercise
{
    public Guid UserId { get; set; }
}
