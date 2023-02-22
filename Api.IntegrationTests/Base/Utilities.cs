using Domain.Entities.Schedule;
using Persistence;

namespace Api.IntegrationTests.Base;

public class Utilities
{
    public static void InitializeDbForTests(GymXptoDbContext context)
    {
        context.Routines.Add(new Routine
        {
            Id = new Guid("5f5606f9-8e09-47d8-8fe0-6cbad8ab49e5"),
            Title = "Rotina do BestOf",
            Description = "um treino diferente para cada dia da semana"
        });
        context.Routines.Add(new Routine
        {
            Id = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c"),
            Title = "Rotina do BodyStation",
            Description = "um treino de lower body e um treino de upper body"
        });
        context.Routines.Add(new Routine
        {
            Id = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = "A minha rotina",
            Description = "treino construído com base em exemplos retirados da internet"
        });

        context.SaveChanges();
    }
}
