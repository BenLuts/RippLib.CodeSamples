using OutsideTestingDemo.Tests.Bootstrapping;
using OutsideTestingDemo.Tests.Contracts;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;

namespace OutsideTestingDemo;

[Collection(nameof(TestFixtureWithSqlServer))]
public class SQLServerTests
{
    private readonly TestFixtureWithSqlServer _fixture;
    private readonly HttpClient _api;
    private readonly HttpClient _unAuthenticatedApi;
    private readonly HttpClient _unPermittedApi;

    public SQLServerTests(TestFixtureWithSqlServer fixture)
    {
        _fixture = fixture;
        _api = _fixture.GetAuthenticatedApiClient();
        _unAuthenticatedApi = fixture.GetApiClient();
        _unPermittedApi = fixture.GetUnpermittedApiClient();
    }

    [Fact]
    public async Task Get_ToWeatherForecastApi_ShouldReturnSuccessfully()
    {
        var response = await _api.GetAsync("weatherforecasts");

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Get_ToWeatherForecastApi_ShouldReturnAtLeastOneResult()
    {
        await _fixture.SeedDatabaseAsync();
        var response = await _api.GetAsync("weatherforecasts");

        var forecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();

        Assert.NotEmpty(forecasts);
    }

    [Fact]
    public async Task AddWeatherForecast_WithValidRequest_ReturnsOk()
    {
        var validWeatherForecast = GetValidWeatherForecast();

        var addResponse = await _api.PostAsJsonAsync("weatherforecasts", validWeatherForecast);

        Assert.Equal(HttpStatusCode.OK, addResponse.StatusCode);
    }

    [Fact]
    public async Task AddWeatherForecast_WithValidRequest_CanBeRetrievedById()
    {
        var weatherForecastId = await CreateValidWeatherForecast();

        var weatherForecast = await _api.GetFromJsonAsync<WeatherForecast>($"weatherforecasts/{weatherForecastId}");
        Assert.Equal(weatherForecastId, weatherForecast.Id);
    }

    [Fact]
    public async Task ReadWeatherForecast_WithoutCredentials_ReturnsUnauthorized()
    {
        var response = await _unAuthenticatedApi.GetAsync("weatherforecasts");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ReadWeatherForecast_WithWrongCredentials_ReturnsForbidden()
    {
        var response = await _unPermittedApi.GetAsync("weatherforecasts");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }



    private static object GetValidWeatherForecast()
    {
        return new
        {
            date = DateOnly.ParseExact("12/06/2025", "dd/MM/yyyy", CultureInfo.GetCultureInfo("nl-BE")),
            temperatureC = 23,
            summary = "Beautiful sunny day, ideal motorcycle weather"
        };
    }

    private async Task<Guid> CreateValidWeatherForecast()
    {
        var validWeatherForecast = GetValidWeatherForecast();
        var addResponse = await _api.PostAsJsonAsync("weatherforecasts", validWeatherForecast);
        return await addResponse.Content.ReadFromJsonAsync<Guid>();
    }
}
