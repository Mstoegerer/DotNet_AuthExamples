using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorizationHandler.Authorization;

public class AuthHandler : AuthorizationHandler<AuthEmployeeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        AuthEmployeeRequirement employeeRequirement)
    {
        if (context.User.HasClaim(c => c.Type == CustomClaims.EmployeeClaim && c.Value == "true"))
            context.Succeed(employeeRequirement);
        else
            context.Fail();

        return Task.CompletedTask;
    }
}