using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OutsideInTestingDemo.App.DataLayer.Interfaces;

namespace OutsideInTestingDemo.App.Logic;

public class WeatherForecastEndpoints : ICarterModule
{
    private const string RequiredRole = "Required.Role";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("weatherforecasts", async (IWeatherForecasts weatherForecasts) =>
        {
            var forecasts = await weatherForecasts.ReadAsync();
            return Results.Ok(forecasts);
        }).RequireAuthorization(RequiredRole);

        app.MapGet("weatherforecasts/{id:guid}", async (Guid id, IWeatherForecasts weatherForecasts) =>
        {
            var forecasts = await weatherForecasts.ReadAsync(id);
            if (forecasts is null)
                return Results.NotFound();
            return Results.Ok(forecasts);
        }).RequireAuthorization(RequiredRole);

        app.MapPost("weatherforecasts", async ([FromBody] WeatherForecastModel forecast, IWeatherForecasts weatherForecasts) =>
        {
            var weatherForecastId = await weatherForecasts.Create(forecast);
            return Results.Ok(weatherForecastId);
        }).RequireAuthorization(RequiredRole);
    }
}
