using Carter;
using OutsideInTestingDemo.App.DataLayer;

namespace OutsideInTestingDemo.App.PostgreSql;

public partial class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCarter();
        builder.AddDataLayerPostgreSql<PostgreDBContext>();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.MapCarter();

        await app.RunAsync();
    }
}
