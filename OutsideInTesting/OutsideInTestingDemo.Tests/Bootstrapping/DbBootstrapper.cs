using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OutsideInTestingDemo.App.DataLayer;
using OutsideInTestingDemo.App.DataLayer.Entities;

namespace OutsideTestingDemo.Tests.Bootstrapping;

public sealed class DbBootstrapper<TApi, TContext> where TApi : class
{
    private readonly ApiFactory<TApi> _apiFactory;

    public DbBootstrapper(ApiFactory<TApi> apiFactory)
    {
        _apiFactory = apiFactory;
    }

    public async Task InitializeDatabaseAsync()
    {
        using var scope = _apiFactory.Services.CreateScope();
        var context = GetContext(scope);

        await context.Database.MigrateAsync();
    }

    public async Task ApplySeed()
    {
        using var scope = _apiFactory.Services.CreateScope();
        var context = GetContext(scope);

        var forecasts = context.Set<WeatherForecast>();

        if (await forecasts.AnyAsync())
        {
            context.RemoveRange(context.Set<WeatherForecast>());
            await context.SaveChangesAsync();
        }

        await forecasts.AddRangeAsync(
            new WeatherForecast()
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now),
                Summary = "Chilly",
                TemperatureC = 5,
            },
            new WeatherForecast()
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now),
                Summary = "Hot",
                TemperatureC = 28,
            }
        );

        await context.SaveChangesAsync();
    }

    private static AppDbContext GetContext(IServiceScope scope)
    {
        return scope.ServiceProvider.GetRequiredService<TContext>() as AppDbContext;
    }
}
