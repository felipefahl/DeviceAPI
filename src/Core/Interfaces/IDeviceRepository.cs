using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IDeviceRepository
    {
        Task<int> SaveChangesAsync();
        Task AddDeviceAsync(Device device);
        Task DeleteDeviceAsync(Device device);
        Task UpdateDeviceAsync(Device device);
        Task<IReadOnlyList<Device>> GetAllAsync();
        Task<IReadOnlyList<Device>> GetFilteredAsync(Expression<Func<Device, bool>> predicate);
        Task<Device?> GetByIdAsync(Guid id);
    }
}
