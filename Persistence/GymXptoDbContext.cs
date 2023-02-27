using Domain.Common;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Persistence;

public class GymXptoDbContext : DbContext
{
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    
    public GymXptoDbContext(DbContextOptions<GymXptoDbContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymXptoDbContext).Assembly);

        //seed data
        //modelBuilder.Entity<Routine>().HasData(new Routine
        //{
        //    RoutineId = new Guid("5f5606f9-8e09-47d8-8fe0-6cbad8ab49e5"),
        //    Title = "Rotina do BestOf",
        //    Description = "um treino diferente para cada dia da semana"
        //});
        //modelBuilder.Entity<Routine>().HasData(new Routine
        //{
        //    RoutineId = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c"),
        //    Title = "Rotina do BodyStation",
        //    Description = "um treino de lower body e um treino de upper body"
        //});
        //modelBuilder.Entity<Routine>().HasData(new Routine
        //{
        //    RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
        //    Title = "A minha rotina",
        //    Description = "treino construído com base em exemplos retirados da internet"
        //});

        //modelBuilder.Entity<Workout>().HasData(new Workout
        //{
        //    WorkoutId = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83"),
        //    RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
        //    Title = "Lower body"
        //});
        //modelBuilder.Entity<Workout>().HasData(new Workout
        //{
        //    WorkoutId = new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164"),
        //    RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
        //    Title = "Upper body"
        //});
        //modelBuilder.Entity<Workout>().HasData(new Workout
        //{
        //    WorkoutId = new Guid("f4370eb8-2a9f-42f5-8d0b-3190d3760db5"),
        //    RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
        //    Title = "Abs day"
        //});
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "tf790515";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "tf790515";
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
