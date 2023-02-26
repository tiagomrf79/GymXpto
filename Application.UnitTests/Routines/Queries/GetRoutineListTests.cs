﻿using Application.Features.Routines.Queries.GetRoutinesList;
using Application.Interfaces.Persistence;
using Application.UnitTests.Common;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Routines.Queries;

[Collection(nameof(DataCollection))]
public class GetRoutineListTests
{
    private readonly Mock<IRoutineRepository> _mockRoutineRepository;
    private readonly IMapper _mapper;

    public GetRoutineListTests(TestFixture testDataFixture)
    {
        _mockRoutineRepository = testDataFixture.MockRoutineRepository;
        _mapper = testDataFixture.Mapper;
    }

    [Fact]
    public async Task Handle_RoutinesListReturned()
    {
        var handler = new GetRoutinesListQueryHandler(_mockRoutineRepository.Object, _mapper);
        var command = new GetRoutinesListQuery();
        int recordCount = (await _mockRoutineRepository.Object.ListAllAsync()).Count;

        var result = await handler.Handle(command, CancellationToken.None);

        result.ShouldBeOfType<List<RoutineListVm>>();
        result.Count.ShouldBe(recordCount);
    }
}
