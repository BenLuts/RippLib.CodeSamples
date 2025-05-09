using OutsideInTestingDemo.App.SqlServer;
using Testcontainers.MsSql;

namespace OutsideTestingDemo.Tests.Bootstrapping;

public sealed class TestFixtureWithSqlServer : TestFixture<Program, SqlServerDBContext>
{
    public override async Task InitializeAsync()
    {
        Environment.SetEnvironmentVariable("DbType", "sqlserver");

        _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();
        await _dbContainer.StartAsync();
        _apiFactory = new ApiFactory<Program>(_dbContainer.GetConnectionString());
        _dbBootstrapper = new(_apiFactory);
        await InitializeDatabaseAsync();
    }
}

[CollectionDefinition(nameof(TestFixtureWithSqlServer))]
public class TestFixtureWithSqlServerCollection : ICollectionFixture<TestFixtureWithSqlServer>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
