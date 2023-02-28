using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Api.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettingstest.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<DbContextOptions<GymXptoDbContext>>()
                .AddDbContext<GymXptoDbContext>((sp, options) =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("GymXptoConnectionString"),
                        builder => builder.MigrationsAssembly(typeof(GymXptoDbContext).Assembly.FullName)));
        });
    }
}
