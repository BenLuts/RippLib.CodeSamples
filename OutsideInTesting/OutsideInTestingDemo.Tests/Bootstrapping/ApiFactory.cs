using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OutsideTestingDemo.Tests.Bootstrapping;

public class ApiFactory<Program> : WebApplicationFactory<Program> where Program : class
{
    private readonly string _dbConnectionstring;

    public ApiFactory(string dbConnectionstring)
    {
        _dbConnectionstring = dbConnectionstring;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings:DefaultConnection", _dbConnectionstring);
    }
}
