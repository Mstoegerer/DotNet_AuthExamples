using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorizationMiddleware.Authorization;

public static class Extensions
{
    public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }

    public static IServiceCollection AddAuthMiddleware(this IServiceCollection services)
    {
        return services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthMiddlewareHandler>();
    }
}