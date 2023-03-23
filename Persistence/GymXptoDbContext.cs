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
        // ROUTINE
        modelBuilder.Entity<Routine>()
            .Property(r => r.Title)
            .HasMaxLength(30);

        // WORKOUT
        modelBuilder.Entity<Workout>()
            .Property(w => w.Title)
            .HasMaxLength(30);

        // EXERCISE
        modelBuilder.Entity<Exercise>()
            .Property(e => e.Name)
            .HasMaxLength(30);
        modelBuilder.Entity<Exercise>()
            .Property(e => e.Instructions)
            .HasMaxLength(500);
        modelBuilder.Entity<Exercise>()
            .Property(e => e.Comments)
            .HasMaxLength(500);

        // MUSCLE
        modelBuilder.Entity<Muscle>()
            .Property(e => e.Name)
            .HasMaxLength(30);

        // EQUIPMENT
        modelBuilder.Entity<Equipment>()
            .Property(e => e.Name)
            .HasMaxLength(30);

        // ROUTINE <=> WORKOUT
        modelBuilder.Entity<Workout>()
            .HasOne(w => w.Routine)
            .WithMany(r => r.Workouts)
            .HasForeignKey(w => w.RoutineId);

        // WORKOUT <=> GROUP
        modelBuilder.Entity<Group>()
            .HasOne(g => g.Workout)
            .WithMany(w => w.ExerciseSequence)
            .HasForeignKey(g => g.WorkoutId);

        // GROUP <=> SUPERSET
        modelBuilder.Entity<Superset>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Sets)
            .HasForeignKey(s => s.GroupId);

        // SUPERSET <=> EXERCISE SET
        modelBuilder.Entity<ExerciseSet>()
            .HasOne(es => es.Superset)
            .WithMany(s => s.ExercisesInSuperset)
            .HasForeignKey(es => es.SupersetId);

        // EXERCISE SET <=> EXERCISE
        // one Exercise can be associated with several Exercise Sets
        modelBuilder.Entity<ExerciseSet>()
            .HasOne(es => es.Exercise)
            .WithMany()
            .HasForeignKey(es => es.ExerciseId);

        // EXERCISE <=> MUSCLE
        // several Exercises can be associated with several Muscles
        modelBuilder.Entity<ExerciseMuscles>()
            .HasKey(em => new { em.ExerciseId, em.MuscleId });
        modelBuilder.Entity<ExerciseMuscles>()
            .HasOne(em => em.Exercise)
            .WithMany(e => e.MusclesWorked)
            .HasForeignKey(em => em.ExerciseId);
        modelBuilder.Entity<ExerciseMuscles>()
            .HasOne(em => em.Muscle)
            .WithMany()
            .HasForeignKey(em => em.MuscleId);

        // EXERCISE <=> EQUIPMENT
        // one Equipment (dumbbells for example) can be associated with several Exercises
        modelBuilder.Entity<Exercise>() //OPTIONAL
            .HasOne(e => e.MainEquipmentUsed)
            .WithMany()
            .HasForeignKey(e => e.MainEquipmentUsedId)
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
