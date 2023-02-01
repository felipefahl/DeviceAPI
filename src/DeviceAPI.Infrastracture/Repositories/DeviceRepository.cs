using Core.Entities;
using Core.Interfaces;
using DeviceAPI.Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DeviceAPI.Infrastracture.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceDatabaseContext _context;

        public DeviceRepository(DeviceDatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task AddDeviceAsync(Device device)
        {
            await _context.Devices.AddAsync(device);
        }

        public Task DeleteDeviceAsync(Device device)
        {
            _context.Devices.Remove(device);
            return Task.CompletedTask;
        }

        public Task UpdateDeviceAsync(Device device)
        {
            _context.Devices.Update(device);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Device>> GetAllAsync()
        {
            return await _context.Devices.ToListAsync();
        }

        public async Task<IReadOnlyList<Device>> GetFilteredAsync(Expression<Func<Device, bool>> predicate)
        {
            return await _context.Devices.Where(predicate).ToListAsync();
        }

        public async Task<Device?> GetByIdAsync(Guid id)
        {
            return await _context.Devices.FindAsync(id);
        }
    }
}
