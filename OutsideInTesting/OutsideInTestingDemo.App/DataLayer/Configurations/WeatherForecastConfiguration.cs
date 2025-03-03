using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutsideInTestingDemo.App.DataLayer.Entities;
namespace OutsideInTestingDemo.App.DataLayer.Configurations;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.Property(x => x.Summary).HasMaxLength(50);
    }
}
