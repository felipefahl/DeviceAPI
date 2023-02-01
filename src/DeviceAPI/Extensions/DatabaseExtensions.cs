namespace DeviceAPI.Extensions;

using DeviceAPI.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public static class DatabaseExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DeviceDatabaseContext>(options => options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("DeviceAPI.Infrastracture");
                }));

        //services.AddDbContext<DeviceDatabaseContext>(options => options
        //    .UseInMemoryDatabase(databaseName: "DevocesDb"));
    }

    public static void UpdateDatabase(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.EnvironmentName != "Test")
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();

            if (serviceScope != null)
            {
                using var context = serviceScope.ServiceProvider.GetRequiredService<DeviceDatabaseContext>();
                context.Database.Migrate();
            }
        }
    }
}
