using Api.IntegrationTests.Common;
using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.DeleteRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Features.Routines.Queries.GetRoutineList;
using Application.Features.Routines.Queries.GetRoutineListWithWorkouts;
using Newtonsoft.Json;
using Shouldly;
using System.Text;

namespace Api.IntegrationTests.Controllers;

public class RoutineControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RoutineControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostRoutine_ValidRoutine_ShouldReturnRoutine()
    {
        var createRoutineCommand = new CreateRoutineCommand()
        {
            Title = "New routine title",
            Description = "New routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(createRoutineCommand), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/routine", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CreateRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<CreateRoutineCommandResponse>();
        result.Success.ShouldBeTrue();
        result.Routine.ShouldNotBeNull();
        result.Routine.Title.ShouldBe(createRoutineCommand.Title);
        result.Routine.Description.ShouldBe(createRoutineCommand.Description);
    }

    [Fact]
    public async Task PostRoutine_InvalidRoutine_ShouldReturnValidationErrors()
    {
        var createRoutineCommand = new CreateRoutineCommand()
        {
            Title = string.Empty,
            Description = "New routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(createRoutineCommand), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/routine", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CreateRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<CreateRoutineCommandResponse>();
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task PutRoutine_ValidRoutine_ShouldReturnRoutine()
    {
        var updateRoutineCommand = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("d9a55fff-ed20-42c9-bf2c-2c2c8c7774aa"),
            Title = "Updated routine title",
            Description = "Updated routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateRoutineCommand), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/routine", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateRoutineCommandResponse>();
        result.Success.ShouldBeTrue();
        result.Routine.ShouldNotBeNull();
        result.Routine.RoutineId.ShouldBe(updateRoutineCommand.RoutineId);
        result.Routine.Title.ShouldBe(updateRoutineCommand.Title);
        result.Routine.Description.ShouldBe(updateRoutineCommand.Description);
    }

    [Fact]
    public async Task PutRoutine_InvalidRoutine_ShouldReturnValidationErrors()
    {
        var updateRoutineCommand = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("d9a55fff-ed20-42c9-bf2c-2c2c8c7774aa"),
            Title = String.Empty,
            Description = "Updated routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateRoutineCommand), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/routine", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateRoutineCommandResponse>();
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task PutRoutine_NotFoundRoutine_ShouldReturnsErrorMessage()
    {
        var updateRoutineCommand = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated routine title",
            Description = "Updated routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateRoutineCommand), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/routine", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateRoutineCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task DeleteRoutine_ValidRoutine_ShouldReturnsSuccess()
    {
        var idToDelete = new Guid("f3f75f23-ae56-4458-8486-9844f6bc0425");

        var response = await _client.DeleteAsync($"api/routine/{idToDelete}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeleteRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<DeleteRoutineCommandResponse>();
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task DeleteRoutine_NotFoundRoutine_ShouldReturnErrorMessage()
    {
        var idToDelete = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var response = await _client.DeleteAsync($"api/routine/{idToDelete}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeleteRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<DeleteRoutineCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetRoutineById_ValidRoutine_ShouldReturnRoutine()
    {
        var idToReturn = new Guid("13aee752-5707-42be-b91e-644eb9bcaf13");

        var response = await _client.GetAsync($"api/routine/{idToReturn}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetRoutineDetailQueryResponse>(responseString);

        result.ShouldBeOfType<GetRoutineDetailQueryResponse>();
        result.Success.ShouldBeTrue();
        result.Routine.ShouldNotBeNull();
        result.Routine.RoutineId.ShouldBe(idToReturn);
    }

    [Fact]
    public async Task GetRoutineById_NotFoundRoutine_ShouldReturnErrorMessage()
    {
        var idToReturn = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var response = await _client.GetAsync($"api/routine/{idToReturn}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetRoutineDetailQueryResponse>(responseString);

        result.ShouldBeOfType<GetRoutineDetailQueryResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetAllRoutines_ShouldReturnList()
    {
        var response = await _client.GetAsync("api/routine/all");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetRoutineListQueryResponse>(responseString);

        result.ShouldBeOfType<GetRoutineListQueryResponse>();
        result.Success.ShouldBeTrue();
        result.RoutineList.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetAllRoutinesWithWorkouts_ShouldReturnListIncludingWorkouts()
    {
        var response = await _client.GetAsync("api/routine/allwithworkouts");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetRoutineListWithWorkoutsQueryResponse>(responseString);

        result.ShouldBeOfType<GetRoutineListWithWorkoutsQueryResponse>();
        result.Success.ShouldBeTrue();
        result.RoutineWorkoutsList.ShouldNotBeNull();
        result.RoutineWorkoutsList.ShouldNotBeEmpty();
        result.RoutineWorkoutsList.ForEach(r => r.Workouts.ShouldNotBeNull());
    }
}
