using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.IntegrationTests.Common;

public static class GymXptoContextFactory
{
    public static GymXptoDbContext Create()
    {
        var dbContextOptions = new DbContextOptionsBuilder<GymXptoDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var _dbContext = new GymXptoDbContext(dbContextOptions);

        var routines = GetData();
        _dbContext.AddRange(routines);
        _dbContext.SaveChanges();

        return _dbContext;
    }

    public static List<Routine> GetData()
    {
        var data = new List<Routine>
        {
            new Routine
            {
                RoutineId = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab"),
                Title = "Routine A title",
                Description = "Routine A description"
            },
            new Routine
            {
                RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
                Title = "Routine B title",
                Description = "Routine B description",
                Workouts =
                {
                    new Workout
                    {
                        WorkoutId = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83"),
                        RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
                        Title = "Workout A title"
                    }
                }
            },
            new Routine
            {
                RoutineId = new Guid("da572ec2-0f0b-4094-bfa7-f51329df41c6"),
                Title = "Routine C title",
                Description = "Routine C description",
                Workouts =
                {
                    new Workout
                    {
                        WorkoutId = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d"),
                        RoutineId = new Guid("da572ec2-0f0b-4094-bfa7-f51329df41c6"),
                        Title = "Workout B title"
                    },
                    new Workout
                    {
                        WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a"),
                        RoutineId = new Guid("da572ec2-0f0b-4094-bfa7-f51329df41c6"),
                        Title = "Workout C title"
                    }
                }
            }
        };

        return data;
    }
}
