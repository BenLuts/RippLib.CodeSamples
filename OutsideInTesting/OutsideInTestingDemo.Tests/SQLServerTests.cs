using OutsideTestingDemo.Tests.Bootstrapping;
using OutsideTestingDemo.Tests.Contracts;
using System.Net.Http.Json;

namespace OutsideTestingDemo;

[Collection(nameof(TestFixtureWithSqlServer))]
public class SQLServerTests
{
    private readonly TestFixtureWithSqlServer _fixture;
    private readonly HttpClient _client;

    public SQLServerTests(TestFixtureWithSqlServer fixture)
    {
        _fixture = fixture;
        _client = _fixture.GetApiClient();
    }

    [Fact]
    public async Task Get_ToWeatherForecastApi_ShouldReturnSuccessfully()
    {
        var response = await _client.GetAsync("weatherforecasts");

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Get_ToWeatherForecastApi_ShouldReturnAtLeastOneResult()
    {
        await _fixture.SeedDatabaseAsync();
        var response = await _client.GetAsync("weatherforecasts");

        var forecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();

        Assert.NotEmpty(forecasts);
    }
}
