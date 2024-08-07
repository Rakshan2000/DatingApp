using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class ApplicationServiceExtentions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

}
