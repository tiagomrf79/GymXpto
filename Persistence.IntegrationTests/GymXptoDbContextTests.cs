/*

Testar métodos do BaseRepository
Testar método do RoutineRepository

*/
using Domain.Entities.Schedule;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Persistence.IntegrationTests.Base;
using Shouldly;
using System.CodeDom;

namespace Persistence.IntegrationTests;

public class GymXptoDbContextTests : IAsyncLifetime
{
    private GymXptoDbContext _gymXptoDbContext;

    public async Task InitializeAsync()
    {
        _gymXptoDbContext = await GymXptoInMemoryContext.GetInMemoryContext();
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

    public Task DisposeAsync() => Task.CompletedTask;
}