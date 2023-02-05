using Core.Entities;
using Core.Interfaces;
using DeviceAPI.Controllers;
using DeviceAPI.Dtos;
using DeviceAPI.UnitTests.Utils.Seeds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceAPI.UnitTests.DeviceAPI.Controllers
{
    public class DevicesControllerTests
    {
        private readonly IDeviceService _deviceService;

        private readonly DevicesController _devicesController;
        private readonly IList<Device> _devices;

        public DevicesControllerTests()
        {
            _devices = DeviceSeeds.Devices;
            _deviceService = Substitute.For<IDeviceService>();

            _devicesController = new DevicesController(_deviceService);
            _devicesController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAllDevices()
        {
            // Arrange
            _deviceService.AllAsync().Returns(_devices);

            // Act
            var result = await _devicesController.ListDevice();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var devices = Assert.IsType<List<Device>>(okResult?.Value);
            Assert.Equal(_devices.Count, devices.Count);
            await _deviceService.Received(1).AllAsync();
            await _deviceService.DidNotReceive().SearchByBrandAsync(Arg.Any<string>());
        }

        [Fact]
        public async Task AddAsync_WhenSearchABrand_ShouldReturnAllBrandDevices()
        {
            // Arrange
            var deviceBrand = _devices.First().Brand;
            var deviceBrandList = _devices.Where(x => x.Brand.Contains(deviceBrand)).ToList();
            _deviceService.SearchByBrandAsync(deviceBrand).Returns(deviceBrandList);

            // Act
            var result = await _devicesController.ListDevice(deviceBrand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var devices = Assert.IsType<List<Device>>(okResult?.Value);
            Assert.Equal(deviceBrandList.Count, devices.Count);
            await _deviceService.DidNotReceive().AllAsync();
            await _deviceService.Received(1).SearchByBrandAsync(deviceBrand);
        }

        [Fact]
        public async Task GetDevice_GivenAnValidId_ShouldReturnADevice()
        {
            // Arrange
            var device = _devices.First();
            var deviceId = device.Id;
            _deviceService.GetByIdAsync(deviceId).Returns(device);

            // Act
            var result = await _devicesController.GetDevice(deviceId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deviceResult = Assert.IsType<Device>(okResult?.Value);
            Assert.Equal(deviceId, deviceResult.Id);
        }

        [Fact]
        public async Task GetDevice_GivenAnInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var deviceId = Guid.NewGuid();

            // Act
            var result = await _devicesController.GetDevice(deviceId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddDevice_GivenAnValidNameAndBrand_ShouldReturnADevice()
        {
            // Arrange
            var device = _devices.First();
            var deviceDto = new DeviceDto
            {
                Name = device.Name,
                Brand = device.Brand
            };
            _deviceService.AddAsync(deviceDto.Name, deviceDto.Brand).Returns(device);

            // Act
            var result = await _devicesController.AddDevice(deviceDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deviceResult = Assert.IsType<Device>(okResult?.Value);
            Assert.Equal(device.Name, deviceResult.Name);
            Assert.Equal(device.Brand, deviceResult.Brand);
        }

        [Fact]
        public async Task AddDevice_GivenAnInvalidNameAndBrand_ShouldReturnStatus422()
        {
            // Arrange
            var deviceDto = new DeviceDto
            {
            };

            // Act
            var result = await _devicesController.AddDevice(deviceDto);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, okResult.StatusCode);
            await _deviceService.DidNotReceive().AddAsync(Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task UpdateDevice_GivenAnValidNameAndBrand_ShouldReturnADevice()
        {
            // Arrange
            var device = _devices.First();
            var deviceId = device.Id;
            var deviceDto = new DeviceDto
            {
                Name = "Name Updated",
                Brand = "Brand Updated"
            };

            device.UpdateBrand(deviceDto.Brand);
            device.UpdateName(deviceDto.Name);

            _deviceService.UpdateAsync(deviceId, deviceDto.Name, deviceDto.Brand).Returns(device);

            // Act
            var result = await _devicesController.UpdateDevice(deviceId, deviceDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deviceResult = Assert.IsType<Device>(okResult?.Value);
            Assert.Equal(device.Name, deviceResult.Name);
            Assert.Equal(device.Brand, deviceResult.Brand);
        }

        [Fact]
        public async Task UpdateDevice_GivenAnInvalidNameAndBrand_ShouldReturnStatus422()
        {
            // Arrange
            var device = _devices.First();
            var deviceId = device.Id;
            var deviceDto = new DeviceDto
            {
            };

            // Act
            var result = await _devicesController.UpdateDevice(deviceId, deviceDto);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, okResult.StatusCode);
            await _deviceService.DidNotReceive().UpdateAsync(deviceId, Arg.Any<string>(), Arg.Any<string>());
        }

        [Fact]
        public async Task UpdateDevice_GivenAnInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceDto = new DeviceDto
            {
                Name = "Name Updated",
                Brand = "Brand Updated"
            };

            // Act
            var result = await _devicesController.UpdateDevice(deviceId, deviceDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBrandAsync_GivenAnValidBrand_ShouldReturnADevice()
        {
            // Arrange
            var device = _devices.First();
            var deviceId = device.Id;
            var deviceDto = new DeviceBrandDto
            {
                Brand = "Brand Updated"
            };

            device.UpdateBrand(deviceDto.Brand);

            _deviceService.UpdateBrandAsync(deviceId, deviceDto.Brand).Returns(device);

            // Act
            var result = await _devicesController.UpdateDeviceBrand(deviceId, deviceDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deviceResult = Assert.IsType<Device>(okResult?.Value);
            Assert.Equal(device.Brand, deviceResult.Brand);
        }

        [Fact]
        public async Task UpdateDeviceBrand_GivenAnInvalidBrand_ShouldReturnStatus422()
        {
            // Arrange
            var device = _devices.First();
            var deviceId = device.Id;
            var deviceDto = new DeviceBrandDto
            {
            };

            // Act
            var result = await _devicesController.UpdateDeviceBrand(deviceId, deviceDto);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, okResult.StatusCode);
            await _deviceService.DidNotReceive().UpdateBrandAsync(deviceId, Arg.Any<string>());
        }

        [Fact]
        public async Task UpdateDeviceBrand_GivenAnInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceDto = new DeviceBrandDto
            {
                Brand = "Brand Updated"
            };

            // Act
            var result = await _devicesController.UpdateDeviceBrand(deviceId, deviceDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateDeviceName_GivenAnValidName_ShouldReturnADevice()
        {
            // Arrange
            var device = _devices.First();
            var deviceId = device.Id;
            var deviceDto = new DeviceNameDto
            {
                Name = "Name Updated",
            };
            device.UpdateName(deviceDto.Name);

            _deviceService.UpdateNameAsync(deviceId, deviceDto.Name).Returns(device);

            // Act
            var result = await _devicesController.UpdateDeviceName(deviceId, deviceDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deviceResult = Assert.IsType<Device>(okResult?.Value);
            Assert.Equal(device.Name, deviceResult.Name);
            Assert.Equal(device.Brand, deviceResult.Brand);
        }

        [Fact]
        public async Task UpdateDeviceName_GivenAnInvalidName_ShouldReturnStatus422()
        {
            // Arrange
            var device = _devices.First();
            var deviceId = device.Id;
            var deviceDto = new DeviceNameDto
            {
            };

            // Act
            var result = await _devicesController.UpdateDeviceName(deviceId, deviceDto);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, okResult.StatusCode);
            await _deviceService.DidNotReceive().UpdateNameAsync(deviceId, Arg.Any<string>());
        }

        [Fact]
        public async Task UpdateDeviceName_GivenAnInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceDto = new DeviceNameDto
            {
                Name = "Name Updated",
            };

            // Act
            var result = await _devicesController.UpdateDeviceName(deviceId, deviceDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }



        [Fact]
        public async Task DeleteDevice_GivenAnValidId_ShouldReturnADevice()
        {
            // Arrange
            var device = _devices.First();
            var deviceId = device.Id;
            _deviceService.DeleteByIdAsync(deviceId).Returns(device);

            // Act
            var result = await _devicesController.DeleteDevice(deviceId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var deviceResult = Assert.IsType<Device>(okResult?.Value);
            Assert.Equal(deviceId, deviceResult.Id);
        }

        [Fact]
        public async Task DeleteDevice_GivenAnInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var deviceId = Guid.NewGuid();

            // Act
            var result = await _devicesController.DeleteDevice(deviceId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
