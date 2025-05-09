using OutsideInTestingDemo.App.Logic;

namespace OutsideInTestingDemo.App.DataLayer.Entities;

public class WeatherForecast
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }

    public static WeatherForecast FromModel(WeatherForecastModel forecastModel)
    {
        return new WeatherForecast()
        {
            Id = Guid.NewGuid(),
            Date = forecastModel.Date,
            TemperatureC = forecastModel.TemperatureC,
            Summary = forecastModel.Summary
        };
    }
}
