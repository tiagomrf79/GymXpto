using Domain.Entities.Schedule;
using Shouldly;

namespace Persistence.IntegrationTests;

[Collection(nameof(DataCollection))]
public class GymXptoDbContextTests
{
    private GymXptoDbContext _gymXptoDbContext;

    public GymXptoDbContextTests(TestFixture testDataFixture)
    {
        _gymXptoDbContext = testDataFixture.GymXptoDbContext;
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
    }

    [Fact]
    public async void Save_ModifiedByPropertySet()
    {
        Routine routineToModify = _gymXptoDbContext.Routines.First();
        routineToModify.Title = "Updated routine title";

        _gymXptoDbContext.Routines.Update(routineToModify);
        await _gymXptoDbContext.SaveChangesAsync();

        routineToModify.LastModifiedBy.ShouldBe("tf790515");
    }
}