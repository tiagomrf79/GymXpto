using Domain.Entities.Schedule;
using Persistence.IntegrationTests.Common;
using Persistence.Repositories;
using Shouldly;

namespace Persistence.IntegrationTests.RepositoriesTests;

public class WorkoutRepositoryTests
{
    private WorkoutRepository _workoutRepository;

    public WorkoutRepositoryTests()
    {
        var dbContext = GymXptoContextFactory.Create();
        _workoutRepository = new WorkoutRepository(dbContext);
    }

    [Fact]
    public async void GetByIdAsync_ValidWorkout_ShouldReturnWorkout()
    {
        var idToLookup = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83");

        var result = await _workoutRepository.GetByIdAsync(idToLookup);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<Workout>();
        result.WorkoutId.ShouldBe(idToLookup);
    }

    [Fact]
    public async void GetByIdAsync_NotFoundWorkout_ShouldReturnNull()
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
        allWorkouts.ShouldBeAssignableTo<IEnumerable<Workout>>();
        allWorkouts.ShouldNotBeEmpty();
    }

    [Fact]
    public async void AddAsync_ValidWorkout_ShouldPersistInDatabase()
    {
        int recordCountBefore = (await _workoutRepository.ListAllAsync()).Count;

        var workoutToAdd = new Workout()
        {
            WorkoutId = Guid.NewGuid(),
            RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = "New workout title"
        };

        await _workoutRepository.AddAsync(workoutToAdd);

        int recordCountAfter = (await _workoutRepository.ListAllAsync()).Count;

        recordCountAfter.ShouldBe(recordCountBefore + 1);
    }

    [Fact]
    public async void AddAsync_ValidWorkout_ShouldReturnWorkout()
    {
        var workoutToAdd = new Workout()
        {
            WorkoutId = Guid.NewGuid(),
            RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = "New workout title"
        };
        var workoutAdded = await _workoutRepository.AddAsync(workoutToAdd);

        workoutAdded.ShouldNotBeNull();
        workoutAdded.ShouldBeOfType<Workout>();
        workoutAdded.WorkoutId.ShouldBe(workoutToAdd.WorkoutId);
        workoutAdded.RoutineId.ShouldBe(workoutToAdd.RoutineId);
        workoutAdded.Title.ShouldBe(workoutToAdd.Title);
    }

    [Fact]
    public async void UpdateAsync_ValidWorkout_ShouldPersistInDatabase()
    {
        var idToUpdate = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83");
        Workout workoutToUpdate = (await _workoutRepository.GetByIdAsync(idToUpdate))!;
        workoutToUpdate.RoutineId = new Guid("d905ff6c-8bf3-4f9f-a275-598e725c6129");
        workoutToUpdate.Title = "Updated workout title";

        await _workoutRepository.UpdateAsync(workoutToUpdate);

        var updatedWorkout = await _workoutRepository.GetByIdAsync(workoutToUpdate.WorkoutId);

        updatedWorkout.ShouldNotBeNull();
        updatedWorkout.ShouldBeOfType<Workout>();
        updatedWorkout.WorkoutId.ShouldBe(workoutToUpdate.WorkoutId);
        updatedWorkout.RoutineId.ShouldBe(workoutToUpdate.RoutineId);
        updatedWorkout.Title.ShouldBe(workoutToUpdate.Title);
    }

    [Fact]
    public async void DeleteAsync_ValidWorkout_ShouldPerishInDatabase()
    {
        int recordCountBefore = (await _workoutRepository.ListAllAsync()).Count;

        Guid idToDelete = new Guid("29f90a63-ea8b-4936-89d5-2e071b4f924d");
        Workout workoutToDelete = (await _workoutRepository.GetByIdAsync(idToDelete))!;
        await _workoutRepository.DeleteAsync(workoutToDelete);

        int recordCountAfter = (await _workoutRepository.ListAllAsync()).Count;

        recordCountAfter.ShouldBe(recordCountBefore - 1);
    }
}
