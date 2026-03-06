using System.Reflection;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration iConfig)
    {
        services.AddAutoMapper(Assembly.GetEntryAssembly());
        services.AddControllers();
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(iConfig.GetConnectionString("DefaultConnection"));
        });
        services.AddCors(options =>
        {
            options.AddPolicy(name: "CORS_1",
            policy =>
            {
                policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });
        });
    }
}