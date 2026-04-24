using Api.Core.Base.API.Middleware;
using Microsoft.AspNetCore.Authentication;

namespace Api.Core.Base.API.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddBasicAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication("Basic")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

        services.AddAuthorization();

        return services;
    }
}
