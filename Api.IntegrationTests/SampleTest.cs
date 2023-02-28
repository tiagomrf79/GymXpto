using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Api.IntegrationTests;

[Collection(nameof(DataCollection))]
public class SampleTest
{
    private static WebApplicationFactory<Program> _factory = null!;

    public SampleTest(TestFixture testDataFixture)
    {
        _factory = testDataFixture._factory;
    }

    [Fact]
    public async Task TestSomething()
    {
        var result = true;

        result.ShouldBeTrue();
    }
}
