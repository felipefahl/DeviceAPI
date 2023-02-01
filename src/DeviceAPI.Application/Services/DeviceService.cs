using Core.Entities;
using Core.Interfaces;
using System.Linq.Expressions;

namespace DeviceAPI.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Device> AddAsync(string name, string brand)
        {
            var device = new Device(name, brand);

            await _deviceRepository.AddDeviceAsync(device);
            await _deviceRepository.SaveChangesAsync();

            return device;
        }

        public async Task<IList<Device>> AllAsync()
        {
            return (await _deviceRepository.GetAllAsync()).ToList();
        }

        public async Task<Device?> DeleteByIdAsync(Guid id)
        {
            var device = await _deviceRepository.GetByIdAsync(id);

            if (device != null)
            {
                await _deviceRepository.DeleteDeviceAsync(device);
                await _deviceRepository.SaveChangesAsync();
            }

            return device;
        }

        public async Task<Device?> GetByIdAsync(Guid id)
        {
            return await _deviceRepository.GetByIdAsync(id);
        }

        public async Task<IList<Device>> SearchByBrandAsync(string brand)
        {
            Expression<Func<Device, bool>> searchBrand = s => s.Brand.Contains(brand);
            return (await _deviceRepository.GetFilteredAsync(searchBrand)).ToList();
        }

        public async Task<Device?> UpdateAsync(Guid id, string name, string brand)
        {
            return await UpdateRepositoryAsync(id, name: name, brand: brand);
        }

        public async Task<Device?> UpdateBrandAsync(Guid id, string brand)
        {
            return await UpdateRepositoryAsync(id, brand: brand);
        }

        public async Task<Device?> UpdateNameAsync(Guid id, string name)
        {            
            return await UpdateRepositoryAsync(id, name: name);
        }

        private async Task<Device?> UpdateRepositoryAsync(Guid id, string? name = null, string? brand = null)
        {
            var device = await _deviceRepository.GetByIdAsync(id);

            if (device != null)
            {
                if (brand != null)
                    device.UpdateBrand(brand);
                
                if (name != null)
                    device.UpdateName(name);
                
                await _deviceRepository.UpdateDeviceAsync(device);
                await _deviceRepository.SaveChangesAsync();
            }

            return device;
        }
    }
}
