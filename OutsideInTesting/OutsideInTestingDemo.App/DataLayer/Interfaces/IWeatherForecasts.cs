using OutsideInTestingDemo.App.Logic;

namespace OutsideInTestingDemo.App.DataLayer.Interfaces;

public interface IWeatherForecasts
{
    Task<WeatherForecastModel> ReadAsync(Guid id);
    Task<List<WeatherForecastModel>> ReadAsync();
    Task<Guid> Create(WeatherForecastModel model);
}
