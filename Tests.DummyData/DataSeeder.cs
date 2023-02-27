using Domain.Entities.Schedule;
using Newtonsoft.Json;

namespace Tests.Common;

public static class DataSeeder
{
    public static List<Routine> GetDummyDataFromJsonFile()
    {
        var data = new List<Routine>();
        var filePath = "JsonFiles/2023_02_26.json";

        if (!File.Exists(filePath))
            throw new IOException("JSON file not found.");

        using (StreamReader reader = new StreamReader(filePath))
        {
            string json = reader.ReadToEnd();
            data = JsonConvert.DeserializeObject<List<Routine>>(json);
        }

        if (data == null)
            throw new ArgumentNullException("Invalid JSON file.");

        return data;
    }
}
