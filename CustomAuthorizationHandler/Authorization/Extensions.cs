using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorizationHandler.Authorization;

public static class Extensions
{
    public static IServiceCollection AddCustomAuthHandler(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.CustomClaimPolicy, policy =>
                policy.RequireCustomClaim());
        });
        services.AddSingleton<IAuthorizationHandler, AuthHandler>();
        return services;
    }

    public static AuthorizationPolicyBuilder RequireCustomClaim(this AuthorizationPolicyBuilder builder)
    {
        return builder.AddRequirements(new AuthEmployeeRequirement());
    }
}