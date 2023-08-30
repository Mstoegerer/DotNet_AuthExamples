using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;

namespace CustomAuthorizationMiddleware.Authorization;

public class AuthMiddlewareHandler : IAuthorizationMiddlewareResultHandler
{
    public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        foreach (var req in policy.Requirements)
            switch (req)
            {
                case RolesAuthorizationRequirement requirement:
                {
                    if (requirement.AllowedRoles.Any(context.User.IsInRole)) return next(context);
                    context.ForbidAsync();
                    return Task.CompletedTask;
                }
                case DenyAnonymousAuthorizationRequirement:
                    Console.WriteLine("DenyAnonymousAuthorizationRequirement");
                    break;
                case ClaimsAuthorizationRequirement requirement:
                {
                    var r = requirement;
                    Console.WriteLine(r.AllowedValues);
                    break;
                }
                case NameAuthorizationRequirement requirement:
                {
                    var r = requirement;
                    break;
                }
                case AssertionRequirement:
                    Console.WriteLine("AssertionRequirement");
                    break;
                case OperationAuthorizationRequirement requirement:
                {
                    var r = requirement;
                    Console.WriteLine(r.Name);
                    break;
                }
                case not null:
                    Console.WriteLine("IAuthorizationRequirement");
                    break;
                default:
                    Console.WriteLine("Unknown");
                    break;
            }

        return Task.CompletedTask;
    }
}