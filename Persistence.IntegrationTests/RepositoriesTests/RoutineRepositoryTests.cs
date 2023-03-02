using Domain.Entities.Schedule;
using Persistence.IntegrationTests.Common;
using Persistence.Repositories;
using Shouldly;

namespace Persistence.IntegrationTests.RepositoriesTests;

public class RoutineRepositoryTests
{
    private RoutineRepository _routineRepository;

    public RoutineRepositoryTests()
    {
        var dbContext = GymXptoContextFactory.Create();
        _routineRepository = new RoutineRepository(dbContext);
    }


    [Fact]
    public async void GetByIdAsync_ValidRoutine_ShouldReturnRoutine()
    {
        var idToLookup = new Guid("da572ec2-0f0b-4094-bfa7-f51329df41c6");

        var result = await _routineRepository.GetByIdAsync(idToLookup);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<Routine>();
        result.RoutineId.ShouldBe(idToLookup);
    }

    [Fact]
    public async void GetByIdAsync_NotFoundRoutine_ShouldReturnNull()
    {
        var idToLookup = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var result = await _routineRepository.GetByIdAsync(idToLookup);

        result.ShouldBeNull();
    }

    [Fact]
    public async void ListAllAsync_ShouldReturnList()
    {
        var allRoutines = await _routineRepository.ListAllAsync();

        allRoutines.ShouldNotBeNull();
        allRoutines.ShouldBeAssignableTo<IEnumerable<Routine>>();
        allRoutines.ShouldNotBeEmpty();
    }

    [Fact]
    public async void AddAsync_ValidRoutine_ShouldPersistInDatabase()
    {
        int recordCountBefore = (await _routineRepository.ListAllAsync()).Count;

        var routineToAdd = new Routine()
        {
            RoutineId = Guid.NewGuid(),
            Title = "New routine title"
        };
        await _routineRepository.AddAsync(routineToAdd);

        int recordCountAfter = (await _routineRepository.ListAllAsync()).Count;

        recordCountAfter.ShouldBe(recordCountBefore + 1);
    }

    [Fact]
    public async void AddAsync_ValidRoutine_ShouldReturnRoutine()
    {
        var routineToAdd = new Routine()
        {
            RoutineId = Guid.NewGuid(),
            Title = "New routine title",
            Description = "New routine description"
        };
        var routineAdded = await _routineRepository.AddAsync(routineToAdd);

        routineAdded.ShouldNotBeNull();
        routineAdded.ShouldBeOfType<Routine>();
        routineAdded.RoutineId.ShouldBe(routineToAdd.RoutineId);
        routineAdded.Title.ShouldBe(routineToAdd.Title);
        routineAdded.Description.ShouldBe(routineToAdd.Description);
    }

    [Fact]
    public async void UpdateAsync_ValidRoutine_ShouldPersistInDatabase()
    {
        var idToUpdate = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad");
        Routine routineToUpdate = (await _routineRepository.GetByIdAsync(idToUpdate))!;
        routineToUpdate.Title = "Updated routine title";
        routineToUpdate.Description = "Updated routine description";

        await _routineRepository.UpdateAsync(routineToUpdate);

        var updatedRoutine = await _routineRepository.GetByIdAsync(routineToUpdate.RoutineId);

        updatedRoutine.ShouldNotBeNull();
        updatedRoutine.ShouldBeOfType<Routine>();
        updatedRoutine.RoutineId.ShouldBe(routineToUpdate.RoutineId);
        updatedRoutine.Title.ShouldBe(routineToUpdate.Title);
        updatedRoutine.Description.ShouldBe(routineToUpdate.Description);
    }

    [Fact]
    public async void DeleteAsync_ValidRoutine_ShouldPerishInDatabase()
    {
        int recordCountBefore = (await _routineRepository.ListAllAsync()).Count;

        Guid idToDelete = new Guid("7b81f3cd-9383-45a9-bfee-0baf166ec6ab");
        Routine routineToDelete = (await _routineRepository.GetByIdAsync(idToDelete))!;
        await _routineRepository.DeleteAsync(routineToDelete);

        int recordCountAfter = (await _routineRepository.ListAllAsync()).Count;

        recordCountAfter.ShouldBe(recordCountBefore - 1);
    }

    [Fact]
    public async void GetRoutineWithWorkouts_ValidRoutine_ShouldReturnListIncludingWorkouts()
    {
        var allRoutines = await _routineRepository.GetRoutineWithWorkouts();

        allRoutines.ShouldNotBeNull();
        allRoutines.ShouldBeAssignableTo<IEnumerable<Routine>>();
        allRoutines.ShouldNotBeEmpty();
        allRoutines.ForEach(r => r.Workouts.ShouldNotBeNull());
    }
}
