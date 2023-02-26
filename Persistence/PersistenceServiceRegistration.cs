﻿using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GymXptoDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GymXptoConnectionString")));

        // generic interfaces can't be added using .AddScoped(IAsyncRepository<T>, BaseRepository<T>)
        // but they can be added using the typeof keyword
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        
        services.AddScoped<IRoutineRepository, RoutineRepository>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();

        return services;
    }
}
