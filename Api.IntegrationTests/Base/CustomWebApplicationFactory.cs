using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Api.IntegrationTests.Base;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddDbContext<GymXptoDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<GymXptoDbContext>();
                //var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                try
                {
                    Utilities.InitializeDbForTests(context);
                }
                catch (Exception ex)
                {
                    //TODO: handle initialize db for tests exception (log)
                    //logger.LogError(ex, $"An error occurred seeding the database with test data. Error: {ex.Message}");
                }
            };
        });
    }


    public HttpClient GetAnonymousClient()
    {
        return CreateClient();
    }
}
