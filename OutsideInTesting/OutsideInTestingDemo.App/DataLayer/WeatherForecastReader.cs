using Microsoft.EntityFrameworkCore;
using OutsideInTestingDemo.App.DataLayer.Entities;
using OutsideInTestingDemo.App.DataLayer.Interfaces;
using OutsideInTestingDemo.App.Logic;

namespace OutsideInTestingDemo.App.DataLayer;

public class WeatherForecastReader<TContext> : IWeatherForecasts where TContext : AppDbContext
{
    private readonly AppDbContext _appDbContext;

    public WeatherForecastReader(TContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<List<WeatherForecastResponse>> ReadWeatherForecastsAsync()
    {
        return _appDbContext.Set<WeatherForecast>()
            .Select(x => new WeatherForecastResponse(x.Date, x.TemperatureC, x.Summary))
            .ToListAsync();
    }
}
