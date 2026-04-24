using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Api.Core.Base.API.Middleware;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeader))
            return Task.FromResult(AuthenticateResult.Fail("Authorization header missing."));

        if (!AuthenticationHeaderValue.TryParse(authHeader, out var headerValue) ||
            !"Basic".Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header."));

        var credentials = Encoding.UTF8
            .GetString(Convert.FromBase64String(headerValue.Parameter ?? string.Empty))
            .Split(':', 2);

        if (credentials.Length != 2)
            return Task.FromResult(AuthenticateResult.Fail("Invalid credentials format."));

        var username = credentials[0];
        var password = credentials[1];

        var validUsername = Configuration["BasicAuth:Username"];
        var validPassword = Configuration["BasicAuth:Password"];

        if (username != validUsername || password != validPassword)
            return Task.FromResult(AuthenticateResult.Fail("Invalid username or password."));

        var claims = new[] { new Claim(ClaimTypes.Name, username) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private IConfiguration Configuration => Context.RequestServices.GetRequiredService<IConfiguration>();
}
