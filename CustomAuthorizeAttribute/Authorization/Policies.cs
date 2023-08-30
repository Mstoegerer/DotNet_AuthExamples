using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorizeAttribute.Authorization;

public static class Policies
{
    public const string AdminPolicy = "Admin";

    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, AdminPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();
        return services;
    }
}