using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API;

public static class IdentityServiceExtentions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var tokenkey = configuration["TokenKey"] ?? throw new Exception("Token Key Not Found");

        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    return services;
    }
}
