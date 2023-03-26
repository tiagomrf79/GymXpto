using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // set up dbcontext to be injected into other parts of the application
        services.AddDbContext<GymXptoDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GymXptoConnectionString")));

        // generic interfaces can't be added using .AddScoped(IAsyncRepository<T>, BaseRepository<T>)
        // but they can be added using the typeof keyword
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        
        services.AddScoped<IRoutineRepository, RoutineRepository>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ISupersetRepository, SupersetRepository>();
        services.AddScoped<IExerciseSetRepository, ExerciseSetRepository>();

        // AddScoped => for database context instances and for services that are expensive to create
        // AddTransient => for lightweight services that don't need to be shared across requests
        // AddSingleton => for services that are expensive to create or that should be shared across the entire application (application configuration settings)

        return services;
    }
}
