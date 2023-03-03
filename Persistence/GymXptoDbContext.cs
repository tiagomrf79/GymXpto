using Domain.Common;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

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
