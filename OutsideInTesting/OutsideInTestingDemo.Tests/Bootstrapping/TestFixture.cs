using DotNet.Testcontainers.Containers;
using OutsideInTestingDemo.Tests.Bootstrapping;
using System.Net.Http.Headers;

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
        var client = _apiFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", FakeJwtTokens.GenerateJwtToken([]));

        return client;
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
