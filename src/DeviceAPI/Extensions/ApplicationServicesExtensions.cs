using Core.Interfaces;
using DeviceAPI.Application.Services;
using DeviceAPI.Infrastracture.Repositories;

namespace DeviceAPI.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            return services;
        }
    }
}
