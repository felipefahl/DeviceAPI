using Core.Entities;

namespace Core.Interfaces
{
    public interface IDeviceService
    {
        Task<Device> AddAsync(string name, string brand);
        Task<Device?> UpdateAsync(Guid id, string name, string brand);
        Task<Device?> UpdateNameAsync(Guid id, string name);
        Task<Device?> UpdateBrandAsync(Guid id, string brand);
        Task<Device?> GetByIdAsync(Guid id);
        Task<Device?> DeleteByIdAsync(Guid id);
        Task<IList<Device>> AllAsync();
        Task<IList<Device>> SearchByBrandAsync(string brand);
    }
}
