using Microsoft.Extensions.DependencyInjection;

namespace Api.Core.Base.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register repositories, external services, DbContext, etc.
        return services;
    }
}
