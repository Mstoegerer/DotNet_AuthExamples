using Microsoft.AspNetCore.Authorization;

namespace PolicyBased.Authorization.Requirements;

public class AdminRequirement : IAuthorizationRequirement
{
}

public class AdminRequirementHandler : AuthorizationHandler<AdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.FailureReasons.Append(new AuthorizationFailureReason(this, "User is not in role Admin"));
            context.Fail();
        }

        return Task.CompletedTask;
    }
}