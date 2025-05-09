using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OutsideInTestingDemo.App.Bootstrapping;
using OutsideInTestingDemo.App.DataLayer;

namespace OutsideInTestingDemo.App.PostgreSql;

public partial class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddDataLayerPostgreSql<PostgreDBContext>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        builder.Services.SetAuthorization();

        builder.Services.AddCarter();
        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapCarter();
        await app.RunAsync();
    }
}
