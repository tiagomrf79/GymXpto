using Application.Interfaces.Persistence;
using AutoMapper;
using Domain.Entities.Schedule;
using Moq;
using Newtonsoft.Json;

namespace Application.UnitTests.Common;

public class TestFixture : IDisposable
{
    public Mock<IRoutineRepository> MockRoutineRepository { get; }
    public Mock<IWorkoutRepository> MockWorkoutRepository { get; }
    public IMapper Mapper { get; }

    public TestFixture()
    {
        var data = new List<Routine>();
        var filePath = "Common/Data/2023_02_26.json";

        if (!File.Exists(filePath))
            throw new IOException("JSON file not found.");

        using (StreamReader reader = new StreamReader(filePath))
        {
            string json = reader.ReadToEnd();
            data = JsonConvert.DeserializeObject<List<Routine>>(json);
        }

        if (data == null)
            throw new ArgumentNullException("Invalid JSON file.");

        MockRoutineRepository = RoutineRepositoryFactory.GetRoutineRepository(data);
        MockWorkoutRepository = WorkoutRepositoryFactory.GetWorkoutRepository(data.SelectMany(r => r.Workouts).ToList());

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
