using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OutsideInTestingDemo.App.DataLayer.Interfaces;

namespace OutsideInTestingDemo.App.DataLayer;

public static class DataLayerDependencyResolution
{
    public static WebApplicationBuilder AddDataLayerSqlServer<TContext>(this WebApplicationBuilder builder) where TContext : AppDbContext
    {
        builder.Services.AddDbContext<TContext>(options =>
        {
            options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
        });

        builder.Services.AddScoped<IWeatherForecasts, WeatherForecastReader<TContext>>();

        return builder;
    }

    public static WebApplicationBuilder AddDataLayerPostgreSql<TContext>(this WebApplicationBuilder builder) where TContext : AppDbContext
    {
        builder.Services.AddDbContext<TContext>(options =>
        {
            var dbDataSource = new NpgsqlDataSourceBuilder(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            options.UseNpgsql(dbDataSource.Build());
        });

        builder.Services.AddScoped<IWeatherForecasts, WeatherForecastReader<TContext>>();

        return builder;
    }
}
