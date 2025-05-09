using DotNet.Testcontainers.Containers;
using OutsideInTestingDemo.Tests.Bootstrapping;
using Respawn;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace OutsideTestingDemo.Tests.Bootstrapping;

public abstract class TestFixture<TApi, TContext> : IAsyncLifetime where TApi : class
{
    protected IDatabaseContainer _dbContainer;
    protected ApiFactory<TApi> _apiFactory;
    protected DbBootstrapper<TApi, TContext> _dbBootstrapper;
    protected Respawner Respawner { get; set; }

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

    public HttpClient GetUnpermittedApiClient()
    {
        var client = GetApiClient();
        var token = FakeJwtToken.GenerateJwtToken([]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }

    public HttpClient GetAuthenticatedApiClient()
    {
        var client = GetApiClient();
        var token = FakeJwtToken.GenerateJwtToken(
        [
            new Claim(ClaimTypes.Role, "Required.Role")
        ]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }

    public virtual async Task SeedDatabaseAsync()
    {
        await Respawner.ResetAsync(_dbContainer.GetConnectionString());
        await _dbBootstrapper.ApplySeed();
    }

    protected virtual async Task InitializeDatabaseAsync()
    {
        Respawner = await Respawner.CreateAsync(_dbContainer.GetConnectionString());
        await _dbBootstrapper.InitializeDatabaseAsync();
    }
}
