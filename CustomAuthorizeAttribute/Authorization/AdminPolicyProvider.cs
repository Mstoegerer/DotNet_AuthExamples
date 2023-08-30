using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CustomAuthorizeAttribute.Authorization;

public class AdminPolicyProvider : IAuthorizationPolicyProvider
{
    public AdminPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        BackupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    private DefaultAuthorizationPolicyProvider BackupPolicyProvider { get; }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
        policy.AddRequirements(new AdminRequirement());
        return Task.FromResult(policy.Build());
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return Task.FromResult(new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser().Build());
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return Task.FromResult<AuthorizationPolicy>(null);
    }
}