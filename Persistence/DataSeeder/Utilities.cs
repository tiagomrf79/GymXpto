using Domain.Entities.Schedule;
using Newtonsoft.Json;

namespace Persistence.DataSeeder;

public class Utilities
{
    public static List<Routine> InitializeDataForTests()
    {
        var data = new List<Routine>();

        using (StreamReader reader = new StreamReader("Mocks/TestingData.json"))
        {
            string json = reader.ReadToEnd();
            data = JsonConvert.DeserializeObject<List<Routine>>(json);
        }

        return data!;
    }
}
