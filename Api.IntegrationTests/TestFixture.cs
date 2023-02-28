using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Tests.Common;

namespace Api.IntegrationTests;

public class TestFixture : IDisposable
{
    public WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    public TestFixture()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();

        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GymXptoDbContext>();

        if (context != null)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            var data = DataSeeder.GetDummyDataFromJsonFile();
            context.AddRange(data);
            context.SaveChanges();
        }


    }

    public void Dispose()
    {
    }
}

[CollectionDefinition(nameof(DataCollection))]
public class DataCollection : ICollectionFixture<TestFixture>
{
}
