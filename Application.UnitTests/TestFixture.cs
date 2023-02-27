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
        List<Routine> routines = DataSeeder.GetDummyDataFromJsonFile();
        List<Workout> workouts = routines.SelectMany(r => r.Workouts).ToList();

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
