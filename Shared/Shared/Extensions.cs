using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Shared;

public static class Extensions
{
    public static byte[] ToUTF8Bytes(this string text)
    {
        var utf8Bytes = Encoding.UTF8.GetBytes(text); // Convert "test" to UTF-8 bytes


        return utf8Bytes;
    }

    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services)
    {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));


        IConfiguration configuration = new ConfigurationManager()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile("appsettings.Development.json", true)
            .AddJsonFile("appsettings.Local.json", true)
            .AddEnvironmentVariables()
            .Build();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var jwtConfig = configuration.GetSection("Jwt").Get<JwtConfig>();

            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.Key.ToUTF8Bytes()),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
        return services;
    }
}