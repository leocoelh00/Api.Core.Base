using Microsoft.Extensions.DependencyInjection;

namespace Api.Core.Base.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register application services, use cases, etc.
        return services;
    }
}
