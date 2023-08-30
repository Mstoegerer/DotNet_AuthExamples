using Microsoft.AspNetCore.Authorization;
using PolicyBased.Authorization.Requirements;

namespace PolicyBased.Authorization;

public static class Policies
{
    public const string AdminPolicy = "AdminPolicy";
    public const string AuthorPolicy = "AuthorPolicy";

    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AdminPolicy, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new AdminRequirement());
            });
        });

        services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();
        return services;
    }
}