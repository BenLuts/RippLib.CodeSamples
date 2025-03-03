using Carter;
using OutsideInTestingDemo.App.DataLayer;

namespace OutsideInTestingDemo.App.SqlServer;

public partial class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCarter();
        builder.AddDataLayerSqlServer<SqlServerDBContext>();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.MapCarter();

        await app.RunAsync();
    }
}
