
using Noa.ContactNotifier.Docker.Configuration;

namespace Noa.ContactNotifier.Docker.Services;
/// <summary>
/// Friendly wrapper for CorsPolicy application
/// </summary>
public static class CorsService
{
    private const string CorsPolicy = "CORS_POLICY";
    private const string CorsCredentials = "CORS_CREDENTIALS";

    public static IServiceCollection AddCorsHandling(this IServiceCollection services, ServiceConfiguration config)
        => services.AddCors(o =>
        {
            o.AddPolicy(CorsPolicy, b =>
            {
                b.AllowAnyMethod().
                AllowAnyHeader().
                AllowCredentials().
                WithOrigins(config.Api.CorsDomain);
            });

            o.AddPolicy(CorsCredentials, b =>
            {
                b.AllowAnyMethod().
                AllowAnyHeader().
                AllowCredentials().
                SetIsOriginAllowed(hostName => true);
            });
        });

    public static IApplicationBuilder UseCorsApplication(this IApplicationBuilder app)
        => app.
        UseCors(CorsCredentials).
        UseCors(CorsPolicy).
        Use(async (context, next) =>
        {
            context.Response.Headers.AccessControlExposeHeaders = "x-suggested-filename";
            await next();
        });
}
