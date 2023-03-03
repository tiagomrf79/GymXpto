using Domain.Entities.Schedule;
using Persistence;
using Tests.Common;

namespace Api.IntegrationTests.Common;

public class Utilities
{
    public static void InitializeDbForTests(GymXptoDbContext context)
    {
        //var data = DataSeeder.GetDummyDataFromJsonFile();
        //context.AddRange(data);
        //context.SaveChanges();

        context.Routines.Add(new Routine
        {
            RoutineId = new Guid("f3f75f23-ae56-4458-8486-9844f6bc0425"),
            Title = "Routine A title",
            Description = "Routine A description"
        });
        context.Routines.Add(new Routine
        {
            RoutineId = new Guid("13aee752-5707-42be-b91e-644eb9bcaf13"),
            Title = "Routine B title",
            Description = "Routine B description"
        });
        context.Routines.Add(new Routine
        {
            RoutineId = new Guid("d9a55fff-ed20-42c9-bf2c-2c2c8c7774aa"),
            Title = "Routine C title",
            Description = "Routine C description"
        });

        context.Workouts.Add(new Workout
        {
            WorkoutId = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83"),
            RoutineId = new Guid("13aee752-5707-42be-b91e-644eb9bcaf13"),
            Title = "Workout A title"
        });
        context.Workouts.Add(new Workout
        {
            WorkoutId = new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164"),
            RoutineId = new Guid("13aee752-5707-42be-b91e-644eb9bcaf13"),
            Title = "Workout B title"
        });
        context.Workouts.Add(new Workout
        {
            WorkoutId = new Guid("f4370eb8-2a9f-42f5-8d0b-3190d3760db5"),
            RoutineId = new Guid("13aee752-5707-42be-b91e-644eb9bcaf13"),
            Title = "Workout C title"
        });

        context.SaveChanges();
    }
}
