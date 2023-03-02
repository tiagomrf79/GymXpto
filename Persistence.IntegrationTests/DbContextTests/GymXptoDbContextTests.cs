using Domain.Entities.Schedule;
using Persistence.IntegrationTests.Common;
using Shouldly;

namespace Persistence.IntegrationTests.DbContextTests;

public class GymXptoDbContextTests
{
    private GymXptoDbContext _gymXptoDbContext;

    public GymXptoDbContextTests()
    {
        _gymXptoDbContext = GymXptoContextFactory.Create();
    }

    [Fact]
    public async void Save_CreatedByPropertySet()
    {
        var routineToCreate = new Routine()
        {
            RoutineId = Guid.NewGuid(),
            Title = "New routine title"
        };

        _gymXptoDbContext.Routines.Add(routineToCreate);
        await _gymXptoDbContext.SaveChangesAsync();

        routineToCreate.CreatedBy.ShouldBe("tf790515");
        routineToCreate.CreatedDate.ShouldBe(DateTime.Now, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async void Save_ModifiedByPropertySet()
    {
        Routine routineToModify = _gymXptoDbContext.Routines.First();
        routineToModify.Title = "Updated routine title";

        _gymXptoDbContext.Routines.Update(routineToModify);
        await _gymXptoDbContext.SaveChangesAsync();

        routineToModify.LastModifiedBy.ShouldBe("tf790515");
        routineToModify.LastModifiedDate.ShouldNotBeNull();
        routineToModify.LastModifiedDate.Value.ShouldBe(DateTime.Now, TimeSpan.FromSeconds(5));
    }
}