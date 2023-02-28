using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Domain.Entities.Schedule;
using Moq;
using Tests.Common;

namespace Application.UnitTests;

public class TestFixture : IDisposable
{
    public Mock<IRoutineRepository> MockRoutineRepository { get; }
    public Mock<IWorkoutRepository> MockWorkoutRepository { get; }
    public IMapper Mapper { get; }

    public TestFixture()
    {
        List<Routine> routines = new List<Routine>();
        List<Workout> workouts = new List<Workout>();

        try
        {
            routines = DataSeeder.GetDummyDataFromJsonFile();
            workouts = routines.SelectMany(r => r.Workouts).ToList();
        }
        catch (Exception ex)
        {
            //TODO: handle get test data from json file exception (log)
            //logger.LogError(ex, $"An error occurred getting test data from file. Error: {ex.Message}");
        }

        MockRoutineRepository = RoutineRepositoryFactory.Create(routines);
        MockWorkoutRepository = WorkoutRepositoryFactory.Create(workouts);

        Mapper = MapperFactory.Create();
    }

    public void Dispose()
    {
    }
}

[CollectionDefinition(nameof(DataCollection))]
public class DataCollection : ICollectionFixture<TestFixture>
{
}
