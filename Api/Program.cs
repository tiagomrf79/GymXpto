using Api;

var builder = WebApplication.CreateBuilder(args);

// call extension methods on the WebApplication
var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

await app.ResetDatabaseAsync();

app.Run();

// used in integration tests
// allows the test code to use the same startup methods as the main application code
// ensuring that the testing environment is consistent with the production/development environment
public partial class Program { }
