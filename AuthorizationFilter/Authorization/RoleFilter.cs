using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthorizationFilter.Authorization;

public class RoleFilter : IAuthorizationFilter
{
    private readonly string[] _roles;

    public RoleFilter(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated == true) return;
        context.Result = new UnauthorizedResult();
        throw new UnauthorizedAccessException("You shall not pass");
    }
}