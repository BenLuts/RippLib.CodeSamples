using Microsoft.Extensions.DependencyInjection;

namespace OutsideInTestingDemo.App.Bootstrapping;
public static class ServiceDefaults
{
    public static void SetAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Required.Role", p =>
            {
                p.RequireRole("Required.Role");
            });
    }
}
