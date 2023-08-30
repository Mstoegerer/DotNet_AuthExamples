namespace ClaimBased.Authorization;

public static class Claims
{
    public const string AdminClaim = "Admin";

    public static IServiceCollection AddClaimAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AdminClaim, policy => policy.RequireClaim(AdminClaim));
        });
        return services;
    }
}