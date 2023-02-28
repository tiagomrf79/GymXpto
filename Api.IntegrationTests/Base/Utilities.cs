using Domain.Entities.Schedule;
using Persistence;
using Tests.Common;

namespace Api.IntegrationTests.Base;

public class Utilities
{
    public static void InitializeDbForTests(GymXptoDbContext context)
    {
        var data = DataSeeder.GetDummyDataFromJsonFile();
        context.AddRange(data);
        context.SaveChanges();

        //context.Routines.Add(new Routine
        //{
        //    RoutineId = new Guid("5f5606f9-8e09-47d8-8fe0-6cbad8ab49e5"),
        //    Title = "Rotina do BestOf",
        //    Description = "um treino diferente para cada dia da semana"
        //});
        //context.Routines.Add(new Routine
        //{
        //    RoutineId = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c"),
        //    Title = "Rotina do BodyStation",
        //    Description = "um treino de lower body e um treino de upper body"
        //});
        //context.Routines.Add(new Routine
        //{
        //    RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
        //    Title = "A minha rotina",
        //    Description = "treino construído com base em exemplos retirados da internet"
        //});

        //context.Workouts.Add(new Workout
        //{
        //    WorkoutId = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83"),
        //    RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
        //    Title = "Lower body"
        //});
        //context.Workouts.Add(new Workout
        //{
        //    WorkoutId = new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164"),
        //    RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
        //    Title = "Upper body"
        //});
        //context.Workouts.Add(new Workout
        //{
        //    WorkoutId = new Guid("f4370eb8-2a9f-42f5-8d0b-3190d3760db5"),
        //    RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
        //    Title = "Abs day"
        //});

        //context.SaveChanges();
    }
}
