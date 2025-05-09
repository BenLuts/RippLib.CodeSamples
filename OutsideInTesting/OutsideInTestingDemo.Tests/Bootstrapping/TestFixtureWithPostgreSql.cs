using Npgsql;
using OutsideInTestingDemo.App.PostgreSql;
using Respawn;
using Testcontainers.PostgreSql;

namespace OutsideTestingDemo.Tests.Bootstrapping;

public sealed class TestFixtureWithPostgreSql : TestFixture<Program, PostgreDBContext>
{
    public override async Task InitializeAsync()
    {
        Environment.SetEnvironmentVariable("DbType", "postgresql");

        _dbContainer = new PostgreSqlBuilder()
            .WithDatabase("WeatherForecast")
            .Build();
        await _dbContainer.StartAsync();
        _apiFactory = new ApiFactory<Program>(_dbContainer.GetConnectionString());
        _dbBootstrapper = new(_apiFactory);

        await InitializeDatabaseAsync();
    }

    protected override async Task InitializeDatabaseAsync()
    {
        await _dbBootstrapper.InitializeDatabaseAsync();

        using var conn = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await conn.OpenAsync();
        Respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
        {
            SchemasToInclude = ["public"],
            DbAdapter = DbAdapter.Postgres
        });
    }

    public override async Task SeedDatabaseAsync()
    {
        using (var conn = new NpgsqlConnection(_dbContainer.GetConnectionString()))
        {
            await conn.OpenAsync();
            await Respawner.ResetAsync(conn);
        }
        await _dbBootstrapper.ApplySeed();
    }
}

[CollectionDefinition(nameof(TestFixtureWithPostgreSql))]
public class TestFixtureWithPostgreSqlCollection : ICollectionFixture<TestFixtureWithPostgreSql>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
