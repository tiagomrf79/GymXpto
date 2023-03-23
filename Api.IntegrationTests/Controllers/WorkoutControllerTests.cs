using Api.IntegrationTests.Common;
using Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;
using Application.Features.Workouts.Commands.CreateWorkout;
using Application.Features.Workouts.Commands.DeleteWorkout;
using Application.Features.Workouts.Commands.UpdateWorkout;
using Application.Features.Workouts.Queries.GetWorkoutDetail;
using Application.Features.Workouts.Queries.GetWorkoutsFromRoutine;
using Newtonsoft.Json;
using Shouldly;
using System.Text;

namespace Api.IntegrationTests.Controllers;

public class WorkoutControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WorkoutControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostWorkout_ValidWorkout_ShouldReturnWorkout()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("13aee752-5707-42be-b91e-644eb9bcaf13"),
            Title = "New workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/workout", jsonContent);

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
    public async Task PostWorkout_NotFoundRoutine_ShouldReturnErrorMessage()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "New workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CreateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<CreateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task PostWorkout_InvalidWorkout_ShouldReturnValidationErrors()
    {
        var command = new CreateWorkoutCommand()
        {
            RoutineId = new Guid("13aee752-5707-42be-b91e-644eb9bcaf13"),
            Title = ""
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CreateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<CreateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task PutWorkout_ValidWorkout_ShouldReturnRoutine()
    {
        var updateWorkoutCommand = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164"),
            RoutineId = new Guid("d9a55fff-ed20-42c9-bf2c-2c2c8c7774aa"),
            Title = "Updated workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateWorkoutCommand), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/workout", jsonContent);

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
    public async Task PutWorkout_NotFoundWorkout_ShouldReturnErrorMessage()
    {
        var updateWorkoutCommand = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            RoutineId = new Guid("d9a55fff-ed20-42c9-bf2c-2c2c8c7774aa"),
            Title = "Updated workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateWorkoutCommand), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task PutWorkout_NotFoundRoutine_ShouldReturnErrorMessage()
    {
        var updateWorkoutCommand = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164"),
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated workout title"
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateWorkoutCommand), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task PutWorkout_InvalidWorkout_ShouldReturnValidationErrors()
    {
        var updateWorkoutCommand = new UpdateWorkoutCommand()
        {
            WorkoutId = new Guid("f4370eb8-2a9f-42f5-8d0b-3190d3760db5"),
            RoutineId = new Guid("d9a55fff-ed20-42c9-bf2c-2c2c8c7774aa"),
            Title = ""
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateWorkoutCommand), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/workout", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task DeleteWorkout_ValidWorkout_ShouldReturnSuccess()
    {
        var idToDelete = new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164");

        var response = await _client.DeleteAsync($"api/workout/{idToDelete}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeleteWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<DeleteWorkoutCommandResponse>();
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task DeleteWorkout_NotFoundWorkout_ShouldReturnErrorMessage()
    {
        var idToDelete = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var response = await _client.DeleteAsync($"api/workout/{idToDelete}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeleteWorkoutCommandResponse>(responseString);

        result.ShouldBeOfType<DeleteWorkoutCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetWorkoutById_ValidWorkout_ShouldReturnWorkout()
    {
        var idToReturn = new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83");

        var response = await _client.GetAsync($"api/workout/{idToReturn}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetWorkoutDetailQueryResponse>(responseString);

        result.ShouldBeOfType<GetWorkoutDetailQueryResponse>();
        result.Success.ShouldBeTrue();
        result.Workout.ShouldNotBeNull();
        result.Workout.WorkoutId.ShouldBe(idToReturn);
    }

    [Fact]
    public async Task GetWorkoutById_NotFoundWorkout_ShouldReturnErrorMessage()
    {
        var idToReturn = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var response = await _client.GetAsync($"api/workout/{idToReturn}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetWorkoutDetailQueryResponse>(responseString);

        result.ShouldBeOfType<GetWorkoutDetailQueryResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetAllWorkouts_ShouldReturnList()
    {
        var routineId = new Guid("13aee752-5707-42be-b91e-644eb9bcaf13");

        var response = await _client.GetAsync($"api/workout/all/{routineId}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetWorkoutsFromRoutineQueryResponse>(responseString);

        result.ShouldBeOfType<GetWorkoutsFromRoutineQueryResponse>();
        result.Success.ShouldBeTrue();
        result.WorkoutList.ShouldNotBeNull();
        result.WorkoutList.ShouldNotBeEmpty();
    }
}
