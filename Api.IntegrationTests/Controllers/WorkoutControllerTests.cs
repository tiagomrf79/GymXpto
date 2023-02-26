using Api.IntegrationTests.Base;
using Application.Features.Workouts.Commands.CreateWorkout;
using Application.Features.Workouts.Commands.DeleteWorkout;
using Application.Features.Workouts.Commands.UpdateWorkout;
using Application.Features.Workouts.Queries.GetWorkoutDetail;
using Newtonsoft.Json;
using Shouldly;
using System.Text;

namespace Api.IntegrationTests.Controllers;

public class WorkoutControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public WorkoutControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AddWorkout_ValidWorkout_ReturnsSuccessResult()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = "New workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PostAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CreateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<CreateWorkoutCommandResponse>();
        result.Success.ShouldBeTrue();
        result.Workout.ShouldNotBeNull();
        result.Workout.RoutineId.ShouldBe(command.RoutineId);
        result.Workout.Title.ShouldBe(command.Title);
    }

    [Fact]
    public async Task AddWorkout_NotFoundRoutine_ReturnsErrorMessage()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "New workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PostAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CreateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<CreateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task AddWorkout_InvalidWorkout_ReturnsValidationErrors()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"),
            Title = ""
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PostAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CreateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<CreateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task UpdateWorkout_ValidWorkout_ReturnsSucessResult()
    {
        var updateWorkoutCommand = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a"),
            RoutineId = new Guid("d905ff6c-8bf3-4f9f-a275-598e725c6129"),
            Title = "Updated workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateWorkoutCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PutAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateWorkoutCommandResponse>();
        result.Success.ShouldBeTrue();
        result.Workout.ShouldNotBeNull();
        result.Workout.WorkoutId.ShouldBe(updateWorkoutCommand.WorkoutId);
        result.Workout.RoutineId.ShouldBe(updateWorkoutCommand.RoutineId);
        result.Workout.Title.ShouldBe(updateWorkoutCommand.Title);
    }

    [Fact]
    public async Task UpdateWorkout_NotFoundWorkout_ReturnsErrorMessage()
    {
        var updateWorkoutCommand = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            RoutineId = new Guid("d905ff6c-8bf3-4f9f-a275-598e725c6129"),
            Title = "Updated workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateWorkoutCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PutAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task UpdateWorkout_NotFoundRoutine_ReturnsErrorMessage()
    {
        var updateWorkoutCommand = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a"),
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateWorkoutCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PutAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task UpdateWorkout_InvalidWorkout_ReturnsValidationErrors()
    {
        var updateWorkoutCommand = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("0867e6ab-8e81-4b6a-9e6c-f2ff30cc5a7a"),
            RoutineId = new Guid("d905ff6c-8bf3-4f9f-a275-598e725c6129"),
            Title = ""
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateWorkoutCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PutAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task DeleteWorkout_ValidWorkout_ReturnsSuccessResult()
    {
        var idToDelete = new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164");

        var client = _factory.GetAnonymousClient();

        var response = await client.DeleteAsync($"api/workout/{idToDelete}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeleteWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<DeleteWorkoutCommandResponse>();
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task DeleteWorkout_NotFoundWorkout_ReturnsErrorMessage()
    {
        var idToDelete = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var client = _factory.GetAnonymousClient();

        var response = await client.DeleteAsync($"api/workout/{idToDelete}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeleteWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<DeleteWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetWorkoutById_ValidWorkout_ReturnsSuccessResult()
    {
        var idToReturn = new Guid("436a8442-3439-4f11-beac-ffc4269c9950");

        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync($"api/workout/{idToReturn}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetWorkoutDetailQueryResponse>(responseString);

        result.ShouldBeOfType<GetWorkoutDetailQueryResponse>();
        result.Success.ShouldBeTrue();
        result.Workout.ShouldNotBeNull();
        result.Workout.WorkoutId.ShouldBe(idToReturn);
    }

    [Fact]
    public async Task GetWorkoutById_NotFoundWorkout_ReturnsErrorMessage()
    {
        var idToReturn = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync($"api/workout/{idToReturn}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetWorkoutDetailQueryResponse>(responseString);

        result.ShouldBeOfType<GetWorkoutDetailQueryResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }
}
