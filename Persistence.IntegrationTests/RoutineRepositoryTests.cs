using Domain.Entities.Schedule;
using Persistence.IntegrationTests.TestData;
using Persistence.Repositories;
using Shouldly;

namespace Persistence.IntegrationTests;

public class RoutineRepositoryTests : IAsyncLifetime
{
    private RoutineRepository _routineRepository;

    public async Task InitializeAsync()
    {
        _routineRepository = await RoutineRepositoryFromMemory.GetRoutineRepository();
    }

    
    [Fact]
    public async void GetByIdAsync_ValidRoutine_RoutineReturned()
    {
        var idToLookup = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c");

        var result = await _routineRepository.GetByIdAsync(idToLookup);

        result.ShouldNotBeNull();
        result.RoutineId.ShouldBe(idToLookup);
    }

    [Fact]
    public async void GetByIdAsync_NotFoundRoutine_NullReturned()
    {
        var idToLookup = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var result = await _routineRepository.GetByIdAsync(idToLookup);

        result.ShouldBeNull();
    }

    [Fact]
    public async void ListAllAsync_ListOfRoutinesReturned()
    {
        var allRoutines = await _routineRepository.ListAllAsync();

        allRoutines.ShouldNotBeNull();
        allRoutines.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async void GetPagedReponseAsync_ValidPageAndSize_ListOfRoutinesReturned()
    {
    }

    [Fact]
    public async void AddAsync_ValidRoutine_RoutineAddedToDatabase()
    {
        var routineToAdd = new Routine()
        {
            RoutineId = Guid.NewGuid(),
            Title = "New routine title"
        };
        int recordCountBefore = (await _routineRepository.ListAllAsync()).Count;

        await _routineRepository.AddAsync(routineToAdd);

        var allRecordsAfter = await _routineRepository.ListAllAsync();
        int recordCountAfter = allRecordsAfter.Count;
        allRecordsAfter.ShouldContain<Routine>(routineToAdd);
        recordCountAfter.ShouldBe(recordCountBefore + 1);
    }

    [Fact]
    public async void AddAsync_ValidRoutine_RoutineReturned()
    {
        var routineToAdd = new Routine()
        {
            RoutineId = Guid.NewGuid(),
            Title = "New routine title",
            Description = "New routine description"
        };

        var routineAdded = await _routineRepository.AddAsync(routineToAdd);

        routineAdded.ShouldNotBeNull();
        routineAdded.RoutineId.ShouldBe(routineToAdd.RoutineId);
        routineAdded.Title.ShouldBe(routineToAdd.Title);
        routineAdded.Description.ShouldBe(routineToAdd.Description);
    }

    [Fact]
    public async void UpdateAsync_ValidRoutine_RoutineUpdatedInDatabase()
    {
        var idToUpdate = new Guid("336b45ac-a39e-46d9-8c47-164240c0fd4c");
        Routine routineToUpdate = (await _routineRepository.GetByIdAsync(idToUpdate))!;
        routineToUpdate.Title = "Updated routine title";
        routineToUpdate.Description = "Updated routine description";
        int recordCountBefore = (await _routineRepository.ListAllAsync()).Count;

        await _routineRepository.UpdateAsync(routineToUpdate);

        int recordCountAfter = (await _routineRepository.ListAllAsync()).Count;
        var updatedRoutine = await _routineRepository.GetByIdAsync(routineToUpdate.RoutineId);
        recordCountAfter.ShouldBe(recordCountBefore);
        updatedRoutine.ShouldNotBeNull();
        updatedRoutine.RoutineId.ShouldBe(routineToUpdate.RoutineId);
        updatedRoutine.Title.ShouldBe(routineToUpdate.Title);
        updatedRoutine.Description.ShouldBe(routineToUpdate.Description);
    }

    [Fact]
    public async void DeleteAsync_ValidRoutine_RoutineRemovedFromDatabase()
    {
        Guid idToDelete = new Guid("5f5606f9-8e09-47d8-8fe0-6cbad8ab49e5");
        Routine routineToDelete = (await _routineRepository.GetByIdAsync(idToDelete))!;
        int recordCountBefore = (await _routineRepository.ListAllAsync()).Count;

        await _routineRepository.DeleteAsync(routineToDelete);

        var allRecordsAfter = await _routineRepository.ListAllAsync();
        int recordCountAfter = allRecordsAfter.Count;
        allRecordsAfter.ShouldNotContain<Routine>(routineToDelete);
        recordCountAfter.ShouldBe(recordCountBefore - 1);
    }

    //GetRoutineWithWorkouts_RoutineHasWorkouts_RoutineWithWorkoutsReturned

    public Task DisposeAsync() => Task.CompletedTask;

}
