using AstroBhaskar.API.Data;
using AstroBhaskar.API.Repositories;
using AstroBhaskar.API.Repositories.Interfaces;
using AstroBhaskar.API.Services;
using AstroBhaskar.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AstroBhaskar.API.Middleware
{
    public static class Registrar
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IUserPermission, UserPermissionService>();
            services.AddScoped<IMasterCollectionService, MasterCollectionService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICommonRepository, CommonRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserPermissionRepository, UserPermissionRepository>()
                .AddScoped<IFirebaseTokenRepository, FirebaseTokenRepository>()
                .AddScoped<IMasterCollectionRepository, MasterCollectionRepository>()
                .AddScoped<ISubscribeNewsLetterRepository, SubscribeNewsLetterRepository>();
        }
        public static void RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString;
            connectionString = configuration.GetConnectionString("DbContext");
            services.AddDbContext<AstroBhaskarDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }

        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AstroBhaskarDbContext>();
            db.Database.Migrate();
        }
    }
}
