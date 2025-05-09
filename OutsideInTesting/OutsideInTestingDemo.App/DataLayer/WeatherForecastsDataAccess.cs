using Microsoft.EntityFrameworkCore;
using OutsideInTestingDemo.App.DataLayer.Entities;
using OutsideInTestingDemo.App.DataLayer.Interfaces;
using OutsideInTestingDemo.App.Logic;

namespace OutsideInTestingDemo.App.DataLayer;

public class WeatherForecastsDataAccess<TContext> : IWeatherForecasts where TContext : AppDbContext
{
    private readonly AppDbContext _appDbContext;

    public WeatherForecastsDataAccess(TContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid> Create(WeatherForecastModel model)
    {
        var forecast = WeatherForecast.FromModel(model);
        await _appDbContext.AddAsync(forecast);
        await _appDbContext.SaveChangesAsync();
        return forecast.Id;
    }

    public Task<List<WeatherForecastModel>> ReadAsync()
    {
        return _appDbContext.Set<WeatherForecast>()
            .Select(x => new WeatherForecastModel(x.Id, x.Date, x.TemperatureC, x.Summary))
            .ToListAsync();
    }

    public Task<WeatherForecastModel> ReadAsync(Guid id)
    {
        return _appDbContext.Set<WeatherForecast>()
            .Where(x => x.Id == id)
            .Select(x => new WeatherForecastModel(x.Id, x.Date, x.TemperatureC, x.Summary))
            .SingleOrDefaultAsync();
    }
}
