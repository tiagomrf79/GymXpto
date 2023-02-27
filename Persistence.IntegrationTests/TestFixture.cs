using Persistence.IntegrationTests.Common;
using Persistence.Repositories;
using Tests.Common;

namespace Persistence.IntegrationTests;

public class TestFixture : IDisposable
{
    public GymXptoDbContext GymXptoDbContext { get; }
    public RoutineRepository RoutineRepository { get; }
    public WorkoutRepository WorkoutRepository { get; }

    public TestFixture()
    {
        var data = DataSeeder.GetDummyDataFromJsonFile();
        GymXptoDbContext = GymXptoContextFactory.Create(data);

        RoutineRepository = new RoutineRepository(GymXptoDbContext);
        WorkoutRepository = new WorkoutRepository(GymXptoDbContext);
    }

    public void Dispose()
    {
    }
}

[CollectionDefinition(nameof(DataCollection))]
public class DataCollection : ICollectionFixture<TestFixture>
{
}
