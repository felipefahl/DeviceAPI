using Core.Entities;
using Core.Interfaces;
using DeviceAPI.Application.Services;
using DeviceAPI.UnitTests.Utils.Mocks.Repositories;
using DeviceAPI.UnitTests.Utils.Seeds;

namespace DeviceAPI.UnitTests.DeviceAPI.Application.Services
{
    public class DeviceServiceTests
    {
        private readonly IDeviceRepository _deviceRepository;

        private readonly IDeviceService _service;

        private readonly IList<Device> _devices;

        public DeviceServiceTests()
        {
            _devices = DeviceSeeds.Devices;

            _deviceRepository = new DeviceRepositoryMock(_devices);

            _service = new DeviceService(_deviceRepository);
        }

        [Fact]
        public async Task AddAsync_GivenAnValidNameAndBrand_ShouldReturnADevice()
        {
            // Arrange
            var name = "Name device";
            var brand = "Brand device";

            // Act
            var result = await _service.AddAsync(name, brand);

            // Assert
            Assert.IsType<Device>(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(brand, result.Brand);
        }

        [Fact]
        public async Task AllAsync_ShouldReturnAllDevices()
        {
            // Arrange
            var devices = _devices;

            // Act
            var result = await _service.AllAsync();

            // Assert
            Assert.Equal(devices.Count(), result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_GivenAnValidId_ShouldReturnADevice()
        {
            // Arrange
            var deviceId = _devices.First().Id;

            // Act
            var result = await _service.GetByIdAsync(deviceId);

            // Assert
            Assert.Equal(deviceId, result?.Id);
        }

        [Fact]
        public async Task GetByIdAsync_GivenAnInvalidId_ShouldReturnNull()
        {
            // Arrange
            var deviceId = Guid.NewGuid();

            // Act
            var result = await _service.GetByIdAsync(deviceId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteByIdAsync_GivenAnValidId_ShouldReturnADevice()
        {
            // Arrange
            var deviceId = _devices.First().Id;

            // Act
            var result = await _service.DeleteByIdAsync(deviceId);

            // Assert
            var deviceDeleted = await _service.GetByIdAsync(deviceId);

            Assert.Equal(deviceId, result?.Id);
            Assert.Null(deviceDeleted);
        }

        [Fact]
        public async Task DeleteByIdAsync_GivenAnInvalidId_ShouldReturnNull()
        {
            // Arrange
            var deviceId = Guid.NewGuid();

            // Act
            var result = await _service.DeleteByIdAsync(deviceId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SearchByBrandAsync_WhenFoundBrand_ShouldReturnAllBrandDevices()
        {
            // Arrange
            var deviceBrand = _devices.First().Brand;
            var deviceBrandCount = _devices.Where(x => x.Brand.Contains(deviceBrand)).Count();

            // Act
            var result = await _service.SearchByBrandAsync(deviceBrand);

            // Assert
            Assert.Equal(deviceBrandCount, result.Count());
        }

        [Fact]
        public async Task SearchByBrandAsync_WhenNotFoundBrand_ShouldReturnEmpty()
        {
            // Arrange
            var deviceBrand = "Not Found Brand";

            // Act
            var result = await _service.SearchByBrandAsync(deviceBrand);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsync_GivenAnValidIdAndNameAndBrand_ShouldReturnADevice()
        {
            // Arrange
            var name = "Name device UPDATED";
            var brand = "Brand device UPDATED";

            var deviceId = _devices.First().Id;

            // Act
            var result = await _service.UpdateAsync(deviceId, name, brand);

            // Assert
            var deviceUpdated = await _service.GetByIdAsync(deviceId);

            Assert.Equal(deviceId, result?.Id);
            Assert.Equal(name, deviceUpdated?.Name);
            Assert.Equal(brand, deviceUpdated?.Brand);
        }

        [Fact]
        public async Task UpdateAsync_GivenAnInvalidId_ShouldReturnNull()
        {
            // Arrange
            var name = "Name device UPDATED";
            var brand = "Brand device UPDATED";
            var deviceId = Guid.NewGuid();

            // Act
            var result = await _service.UpdateAsync(deviceId, name, brand);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateNameAsync_GivenAnValidIdAndName_ShouldReturnADevice()
        {
            // Arrange
            var name = "Name device UPDATED";

            var deviceId = _devices.First().Id;

            // Act
            var result = await _service.UpdateNameAsync(deviceId, name);

            // Assert
            var deviceUpdated = await _service.GetByIdAsync(deviceId);

            Assert.Equal(deviceId, result?.Id);
            Assert.Equal(name, deviceUpdated?.Name);
        }

        [Fact]
        public async Task UpdateNameAsync_GivenAnInvalidId_ShouldReturnNull()
        {
            // Arrange
            var name = "Name device UPDATED";
            var deviceId = Guid.NewGuid();

            // Act
            var result = await _service.UpdateNameAsync(deviceId, name);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBrandAsync_GivenAnValidIdAndBrand_ShouldReturnADevice()
        {
            // Arrange
            var brand = "Brand device UPDATED";

            var deviceId = _devices.First().Id;

            // Act
            var result = await _service.UpdateBrandAsync(deviceId, brand);

            // Assert
            var deviceUpdated = await _service.GetByIdAsync(deviceId);

            Assert.Equal(deviceId, result?.Id);
            Assert.Equal(brand, deviceUpdated?.Brand);
        }

        [Fact]
        public async Task UpdateBrandAsync_GivenAnInvalidId_ShouldReturnNull()
        {
            // Arrange
            var brand = "Brand device UPDATED";
            var deviceId = Guid.NewGuid();

            // Act
            var result = await _service.UpdateBrandAsync(deviceId, brand);

            // Assert
            Assert.Null(result);
        }
    }
}
