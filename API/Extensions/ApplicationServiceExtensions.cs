using System.Reflection;
using API.Data;
using API.Interfaces;
using API.Repository;
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
                policy.WithOrigins("http://localhost:4200", "http://192.168.100.126:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
            });
        });

        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IChatParticipantRepository, ChatParticipantRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
    }
}