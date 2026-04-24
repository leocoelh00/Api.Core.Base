using Api.Core.Base.API.Filters;
using Microsoft.OpenApi;

namespace Api.Core.Base.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api.Core.Base",
                Version = "v1",
                Description = "Base API template with Clean Architecture"
            });

            options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                Description = "Basic Authentication — informe usuário e senha"
            });

            options.OperationFilter<BasicAuthOperationFilter>();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.Core.Base v1");
            options.RoutePrefix = "swagger";
        });

        return app;
    }
}
