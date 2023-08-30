using Microsoft.AspNetCore.Mvc;

namespace AuthorizationFilter.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class RoleFilterAttribute : TypeFilterAttribute
{
    public RoleFilterAttribute(params string[] roles) : base(typeof(RoleFilter))
    {
        Arguments = new object[] { roles };
    }
}