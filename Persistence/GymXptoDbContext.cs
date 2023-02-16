using Domain.Common;
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class GymXptoDbContext : DbContext
{
    public DbSet<Routine> Routines { get; set; }
    
    public GymXptoDbContext(DbContextOptions<GymXptoDbContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymXptoDbContext).Assembly);

        //seed data
        modelBuilder.Entity<Routine>().HasData(new Routine
        {
            Id = Guid.NewGuid(),
            Title = "Rotina do BestOf",
            Description = "um treino diferente para cada dia da semana"
        });
        modelBuilder.Entity<Routine>().HasData(new Routine
        {
            Id = Guid.NewGuid(),
            Title = "Rotina do BodyStation",
            Description = "um treino de lower body e um treino de upper body"
        });
        modelBuilder.Entity<Routine>().HasData(new Routine
        {
            Id = Guid.NewGuid(),
            Title = "A minha rotina",
            Description = "treino construído com base em exemplos retirados da internet"
        });
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
