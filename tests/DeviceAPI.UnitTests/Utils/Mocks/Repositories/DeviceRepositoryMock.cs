using Core.Entities;
using Core.Interfaces;
using System.Linq.Expressions;

namespace DeviceAPI.UnitTests.Utils.Mocks.Repositories
{
    public class DeviceRepositoryMock : IDeviceRepository
    {

        private readonly IList<Device> _devices;

        public DeviceRepositoryMock(IList<Device> devices)
        {
            if (_devices == null || !_devices.Any())
            {
                _devices = devices;
            }
        }

        public Task AddDeviceAsync(Device device)
        {
            _devices.Add(device);
            return Task.CompletedTask;
        }

        public Task DeleteDeviceAsync(Device device)
        {
            _devices.Remove(device);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Device>> GetAllAsync()
        {
            return Task.FromResult(_devices.ToList() as IReadOnlyList<Device>);
        }

        public Task<Device?> GetByIdAsync(Guid id)
        {
            var deviceFound = _devices
                .Where(x => x.Id == id)
                .SingleOrDefault();

            return Task.FromResult(deviceFound);
        }

        public Task<IReadOnlyList<Device>> GetFilteredAsync(Expression<Func<Device, bool>> predicate)
        {
            var listFiltered = _devices
                .AsQueryable()
                .Where(predicate)
                .ToList();
            return Task.FromResult(listFiltered as IReadOnlyList<Device>);
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }

        public Task UpdateDeviceAsync(Device device)
        {
            _devices
                .Where(x => x.Id == device.Id)
                .ToList()
                .ForEach(x => x = device);

            return Task.CompletedTask;
        }
    }
}
