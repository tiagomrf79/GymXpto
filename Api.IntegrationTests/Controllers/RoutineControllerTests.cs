﻿using Api.IntegrationTests.Base;
using Application.Features.Routines.Commands.CreateRoutine;
using Application.Features.Routines.Commands.DeleteRoutine;
using Application.Features.Routines.Commands.UpdateRoutine;
using Application.Features.Routines.Queries.GetRoutineDetail;
using Application.Features.Routines.Queries.GetRoutinesList;
using Application.Features.Routines.Queries.GetRoutinesListWithWorkouts;
using Newtonsoft.Json;
using Shouldly;
using System.Text;

namespace Api.IntegrationTests.Controllers;

public class RoutineControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public RoutineControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AddRoutine_ValidRoutine_ReturnsSuccessResult()
    {
        var createRoutineCommand = new CreateRoutineCommand()
        {
            Title = "New routine title",
            Description = "New routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(createRoutineCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PostAsync("api/routine", jsonContent);

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
    public async Task AddRoutine_InvalidRoutine_ReturnsFailureResult()
    {
        var createRoutineCommand = new CreateRoutineCommand()
        {
            Title = string.Empty,
            Description = "New routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(createRoutineCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PostAsync("api/routine", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CreateRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<CreateRoutineCommandResponse>();
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task UpdateRoutine_ValidRoutine_ReturnsSuccessResult()
    {
        var updateRoutineCommand = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
            Title = "Updated routine title",
            Description = "Updated routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateRoutineCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PutAsync("api/routine", jsonContent);

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
    public async Task UpdateRoutine_InvalidRoutine_ReturnsFailureResult()
    {
        var updateRoutineCommand = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("3c4854c9-cab8-4555-9d4d-dcf107ce61ad"),
            Title = String.Empty,
            Description = "Updated routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateRoutineCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PutAsync("api/routine", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateRoutineCommandResponse>();
        result.Success.ShouldBeFalse();
        result.ValidationErrors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task UpdateRoutine_NotFoundRoutine_ReturnsFailureResult()
    {
        var updateRoutineCommand = new UpdateRoutineCommand()
        {
            RoutineId = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb"),
            Title = "Updated routine title",
            Description = "Updated routine description"
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(updateRoutineCommand), Encoding.UTF8, "application/json");

        var client = _factory.GetAnonymousClient();

        var response = await client.PutAsync("api/routine", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<UpdateRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<UpdateRoutineCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task DeleteRoutine_ValidRoutine_ReturnsSuccessResult()
    {
        var idToDelete = new Guid("5f5606f9-8e09-47d8-8fe0-6cbad8ab49e5");

        var client = _factory.GetAnonymousClient();

        var response = await client.DeleteAsync($"api/routine/{idToDelete}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeleteRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<DeleteRoutineCommandResponse>();
        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task DeleteRoutine_NotFoundRoutine_ReturnsFailureResult()
    {
        var idToDelete = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var client = _factory.GetAnonymousClient();

        var response = await client.DeleteAsync($"api/routine/{idToDelete}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DeleteRoutineCommandResponse>(responseString);

        result.ShouldBeOfType<DeleteRoutineCommandResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetRoutineById_ValidRoutine_ReturnsSuccessResult()
    {
        var idToReturn = new Guid("da572ec2-0f0b-4094-bfa7-f51329df41c6");

        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync($"api/routine/{idToReturn}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetRoutineDetailQueryResponse>(responseString);

        result.ShouldBeOfType<GetRoutineDetailQueryResponse>();
        result.Success.ShouldBeTrue();
        result.Routine.ShouldNotBeNull();
        result.Routine.RoutineId.ShouldBe(idToReturn);
    }

    [Fact]
    public async Task GetRoutineById_NotFoundRoutine_ReturnsFailureResult()
    {
        var idToReturn = new Guid("aabbaabb-aabb-aabb-aabb-aabbaabbaabb");

        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync($"api/routine/{idToReturn}");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<GetRoutineDetailQueryResponse>(responseString);

        result.ShouldBeOfType<GetRoutineDetailQueryResponse>();
        result.Success.ShouldBeFalse();
        result.Message.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetAllRoutines_ReturnsSuccessResult()
    {
        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync("api/routine/all");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<RoutineListVm>>(responseString);

        result.ShouldBeOfType<List<RoutineListVm>>();
        result.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task GetAllRoutinesWithWorkouts_ReturnsSuccessResult()
    {
        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync("api/routine/allwithworkouts");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<RoutineWorkoutsListVm>>(responseString);

        result.ShouldBeOfType<List<RoutineWorkoutsListVm>>();
        result.ShouldNotBeEmpty();
    }
}
