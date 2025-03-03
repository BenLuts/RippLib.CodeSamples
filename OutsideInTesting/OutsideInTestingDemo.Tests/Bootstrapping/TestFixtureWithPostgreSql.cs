using OutsideInTestingDemo.App.PostgreSql;
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
}

[CollectionDefinition(nameof(TestFixtureWithPostgreSql))]
public class TestFixtureWithPostgreSqlCollection : ICollectionFixture<TestFixtureWithPostgreSql>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
