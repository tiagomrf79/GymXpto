using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Api.IntegrationTests.Common;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GymXptoDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            string inMemoryCollectionName = Guid.NewGuid().ToString();
            services.AddDbContext<GymXptoDbContext>(options =>
            {
                options.UseInMemoryDatabase(inMemoryCollectionName);
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                var context = scopedServices.GetRequiredService<GymXptoDbContext>();

                context.Database.EnsureCreated();

                try
                {
                    Utilities.InitializeDbForTests(context);
                }
                catch (Exception ex)
                {
                    //logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                }
            };

        });
    }
}
