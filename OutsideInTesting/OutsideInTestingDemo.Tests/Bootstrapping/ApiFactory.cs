using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OutsideInTestingDemo.Tests.Bootstrapping;

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
        builder.ConfigureTestServices(services =>
        {
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var config = new OpenIdConnectConfiguration()
                {
                    Issuer = FakeJwtToken.Issuer,
                };

                options.Audience = FakeJwtToken.Audience;
                config.SigningKeys.Add(FakeJwtToken.SecurityKey);
                options.Configuration = config;
            });
        });
    }
}
