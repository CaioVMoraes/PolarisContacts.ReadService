using PolarisContacts.ReadService.CrossCutting.DependencyInjection.Extensions.AddInfrastructureLayer;
using Microsoft.Extensions.DependencyInjection;
using PolarisContacts.ReadService.CrossCutting.DependencyInjection.Extensions.AddApplicationLayer;

namespace PolarisContacts.ReadService.CrossCutting.DependencyInjection;

public static class Bootstrap
{
    public static IServiceCollection RegisterServices(this IServiceCollection services) =>
        services
            .AddInfrastructure()
            .AddApplication();
}
