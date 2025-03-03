using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OutsideInTestingDemo.App.DataLayer.Interfaces;

namespace OutsideInTestingDemo.App.Logic;

public class WeatherForecastEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("weatherforecasts", async (IWeatherForecasts weatherForecasts) =>
        {
            var forecasts = await weatherForecasts.ReadWeatherForecastsAsync();
            return Results.Ok(forecasts);
        });
    }
}
