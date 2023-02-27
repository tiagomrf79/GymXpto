using Domain.Entities.Schedule;
using Persistence.Repositories;
using Shouldly;

namespace Persistence.IntegrationTests.RepositoryTests;

[Collection(nameof(DataCollection))]
public class WorkoutRepositoryTests
{
    private WorkoutRepository _workoutRepository;

    public WorkoutRepositoryTests(TestFixture testDataFixture)
    {
        _workoutRepository = testDataFixture.WorkoutRepository;
    }

    [Fact]
    public async void GetByIdAsync_ValidWorkout_WorkoutReturned()
    {
        var idToLookup = new Guid("436a8442-3439-4f11-beac-ffc4269c9950");

        var result = await _workoutRepository.GetByIdAsync(idToLookup);

        result.ShouldNotBeNull();
        result.WorkoutId.ShouldBe(idToLookup);
    }

    [Fact]
    public async void GetByIdAsync_NotFoundWorkout_NullReturned()
    {
        var idToLookup = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var result = await _workoutRepository.GetByIdAsync(idToLookup);

        result.ShouldBeNull();
    }

    [Fact]
    public async void ListAllAsync_ListOfWorkoutsReturned()
    {
        var allWorkouts = await _workoutRepository.ListAllAsync();

        allWorkouts.ShouldNotBeNull();
        allWorkouts.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async void AddAsync_ValidWorkout_WorkoutAddedToDatabase()
    {
        var workoutToAdd = new Workout()
        {
            WorkoutId = Guid.NewGuid(),
            RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = "New workout title"
        };
        int recordCountBefore = (await _workoutRepository.ListAllAsync()).Count;

        await _workoutRepository.AddAsync(workoutToAdd);

        var allRecordsAfter = await _workoutRepository.ListAllAsync();
        int recordCountAfter = allRecordsAfter.Count;
        allRecordsAfter.ShouldContain(workoutToAdd);
        recordCountAfter.ShouldBe(recordCountBefore + 1);
    }

    [Fact]
    public async void AddAsync_ValidWorkout_WorkoutReturned()
    {
        var workoutToAdd = new Workout()
        {
            WorkoutId = Guid.NewGuid(),
            RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = "New workout title"
        };

        var workoutAdded = await _workoutRepository.AddAsync(workoutToAdd);

        workoutAdded.ShouldNotBeNull();
        workoutAdded.WorkoutId.ShouldBe(workoutToAdd.WorkoutId);
        workoutAdded.RoutineId.ShouldBe(workoutToAdd.RoutineId);
        workoutAdded.Title.ShouldBe(workoutToAdd.Title);
    }

    [Fact]
    public async void UpdateAsync_ValidWorkout_WorkoutUpdatedInDatabase()
    {
        var idToUpdate = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83");
        Workout workoutToUpdate = (await _workoutRepository.GetByIdAsync(idToUpdate))!;
        workoutToUpdate.RoutineId = new Guid("d905ff6c-8bf3-4f9f-a275-598e725c6129");
        workoutToUpdate.Title = "Updated workout title";
        int recordCountBefore = (await _workoutRepository.ListAllAsync()).Count;

        await _workoutRepository.UpdateAsync(workoutToUpdate);

        int recordCountAfter = (await _workoutRepository.ListAllAsync()).Count;
        var updatedWorkout = await _workoutRepository.GetByIdAsync(workoutToUpdate.WorkoutId);
        recordCountAfter.ShouldBe(recordCountBefore);
        updatedWorkout.ShouldNotBeNull();
        updatedWorkout.WorkoutId.ShouldBe(workoutToUpdate.WorkoutId);
        updatedWorkout.RoutineId.ShouldBe(workoutToUpdate.RoutineId);
        updatedWorkout.Title.ShouldBe(workoutToUpdate.Title);
    }

    [Fact]
    public async void DeleteAsync_ValidWorkout_WorkoutRemovedFromDatabase()
    {
        Guid idToDelete = new Guid("f4370eb8-2a9f-42f5-8d0b-3190d3760db5");
        Workout workoutToDelete = (await _workoutRepository.GetByIdAsync(idToDelete))!;
        int recordCountBefore = (await _workoutRepository.ListAllAsync()).Count;

        await _workoutRepository.DeleteAsync(workoutToDelete);

        var allRecordsAfter = await _workoutRepository.ListAllAsync();
        int recordCountAfter = allRecordsAfter.Count;
        allRecordsAfter.ShouldNotContain(workoutToDelete);
        recordCountAfter.ShouldBe(recordCountBefore - 1);
    }
}
