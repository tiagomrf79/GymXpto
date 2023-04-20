using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;

namespace Api;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        AddSwagger(builder.Services);

        //configure application layer services
        builder.Services.AddApplicationServices();

        //configure persistence layer services
        builder.Services.AddPersistenceServices(builder.Configuration);

        //adds services and routing logic so controllers can handle requests
        builder.Services.AddControllers();

        //TODO: change CORS policy to a stricter version
        //CORS prevents JavaScript code running on one website from accessing resources on another website without explicit permission
        builder.Services.AddCors(options =>
        {
            //with open policy any client from any domain can access the resources exposed by the application using any HTTP method
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GymXpto API");
            });
        }
        
        //intercepts http requests and redirects them to https (encrypts the transmitted data)
        //comes early to force other middleware to be accessed over a secure connection
        app.UseHttpsRedirection();

        //enables routing: inspects incoming requests and matches them to specific routes defined in the application
        app.UseRouting();

        app.UseCors("Open");

        //maps http requests to actions on controllers that are decorated with the [ApiController] attribute
        app.MapControllers();

        return app;
    }

    public static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "GymXpto API",

            });
        });
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        //creates a new scope within the application's service container
        //when it's disposed, any services created within that scope are also disposed
        using var scope = app.Services.CreateScope();

        try
        {
            //get an instance of the GymXptoDbContext from the service container
            var context = scope.ServiceProvider.GetService<GymXptoDbContext>();
            if (context != null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            //TODO: handle database reset exceptions (log)
        }
    }
}
