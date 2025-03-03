using DotNet.Testcontainers.Containers;

namespace OutsideTestingDemo.Tests.Bootstrapping;

public abstract class TestFixture<TApi, TContext> : IAsyncLifetime where TApi : class
{
    protected IDatabaseContainer _dbContainer;
    protected ApiFactory<TApi> _apiFactory;
    protected DbBootstrapper<TApi, TContext> _dbBootstrapper;

    public abstract Task InitializeAsync();

    public async Task DisposeAsync()
    {
        _dbBootstrapper = null;
        await _apiFactory.DisposeAsync();
        await _dbContainer!.DisposeAsync();
    }

    public HttpClient GetApiClient()
    {
        return _apiFactory.CreateClient();
    }

    public Task SeedDatabaseAsync()
    {
        return _dbBootstrapper.ApplySeed();
    }

    protected Task InitializeDatabaseAsync()
    {
        return _dbBootstrapper.InitializeDatabaseAsync();
    }
}
