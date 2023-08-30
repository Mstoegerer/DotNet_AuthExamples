using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorizeAttribute.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class AdminAttribute : AuthorizeAttribute
{
    public AdminAttribute()
    {
        Policy = Policies.AdminPolicy;
    }
}