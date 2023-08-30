using Microsoft.AspNetCore.Authentication;

namespace CustomAuthorizationMiddleware.Authorization;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;


    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var identity = context.User?.Identity;
        if (identity is not { IsAuthenticated: true }) await context.ChallengeAsync();
        await _next.Invoke(context);
    }
}