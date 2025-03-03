using OutsideInTestingDemo.App.Logic;

namespace OutsideInTestingDemo.App.DataLayer.Interfaces;

public interface IWeatherForecasts
{
    Task<List<WeatherForecastResponse>> ReadWeatherForecastsAsync();
}
