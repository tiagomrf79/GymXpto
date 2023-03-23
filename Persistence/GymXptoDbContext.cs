using Domain.Common;
using Domain.Entities;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class GymXptoDbContext : DbContext
{
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Superset> Supersets { get; set; }
    public DbSet<ExerciseSet> ExerciseSets { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Muscle> Muscles { get; set; }

    
    public GymXptoDbContext(DbContextOptions<GymXptoDbContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymXptoDbContext).Assembly);

        // ROUTINE <=> WORKOUT
        modelBuilder.Entity<Workout>()
            .HasOne(w => w.Routine)
            .WithMany(r => r.Workouts);

        // WORKOUT <=> GROUP
        modelBuilder.Entity<Group>()
            .HasOne(g => g.Workout)
            .WithMany(w => w.ExerciseSequence);

        // GROUP <=> SUPERSET
        modelBuilder.Entity<Superset>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Sets);

        // SUPERSET <=> EXERCISE SET
        modelBuilder.Entity<ExerciseSet>()
            .HasOne(es => es.Superset)
            .WithMany(s => s.ExercisesInSuperset);

        // EXERCISE SET <=> EXERCISE
        // one Exercise can be associated with several Exercise Sets
        modelBuilder.Entity<ExerciseSet>()
            .HasOne(es => es.Exercise)
            .WithMany();

        // EXERCISE <=> MAIN MUSCLE
        // one Muscle can be associated with several Exercises
        modelBuilder.Entity<Exercise>()
            .HasOne(e => e.MainMuscleWorked)
            .WithMany();

        // EXERCISE <=> EQUIPMENT
        // one Equipment (dumbbells for example) can be associated with several Exercises
        modelBuilder.Entity<Exercise>() //OPTIONAL
            .HasOne(e => e.MainEquipmentUsed)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientSetNull);

        // EXERCISE <=> OTHER MUSCLES
        // one Muscle can be associated with several Exercises
        modelBuilder.Entity<Exercise>() //OPTIONAL
            .HasMany(e => e.SynergistsMusclesWorked)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientSetNull);

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
