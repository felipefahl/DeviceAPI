using Core.Entities;
using Core.Interfaces;
using DeviceAPI.Dtos;
using DeviceAPI.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DeviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> ListDevice([FromQuery] string? brand = null)
        {
            IList<Device> result;

            if (string.IsNullOrEmpty(brand))
                result = await _deviceService.AllAsync();

            else
                result = await _deviceService.SearchByBrandAsync(brand);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice([FromRoute] Guid id)
        {
            var result = await _deviceService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(NotFoundMessage(id));
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] DeviceDto device)
        {

            var errors = GetValidationRequestErrors<DeviceDtoValidation, DeviceDto>(device);

            if (errors.Any())
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, GetUnprocessableEntityResponse(HttpContext, errors));
            }

            var result = await _deviceService.AddAsync(device.Name, device.Brand);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice([FromRoute] Guid id, [FromBody] DeviceDto device)
        {
            var errors = GetValidationRequestErrors<DeviceDtoValidation, DeviceDto>(device);

            if (errors.Any())
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, GetUnprocessableEntityResponse(HttpContext, errors));
            }

            var result = await _deviceService.UpdateAsync(id, device.Name, device.Brand);

            if (result == null)
            {
                return NotFound(NotFoundMessage(id));
            }

            return Ok(result);
        }

        [HttpPatch("{id}/name")]
        public async Task<IActionResult> UpdateDeviceName([FromRoute] Guid id, [FromBody] DeviceNameDto deviceName)
        {
            var errors = GetValidationRequestErrors<DeviceNameDtoValidation, DeviceNameDto>(deviceName);

            if (errors.Any())
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, GetUnprocessableEntityResponse(HttpContext, errors));
            }

            var result = await _deviceService.UpdateNameAsync(id, deviceName.Name);

            if (result == null)
            {
                return NotFound(NotFoundMessage(id));
            }

            return Ok(result);
        }

        [HttpPatch("{id}/brand")]
        public async Task<IActionResult> UpdateDeviceBrand([FromRoute] Guid id, [FromBody] DeviceBrandDto deviceBrand)
        {
            var errors = GetValidationRequestErrors<DeviceBrandDtoValidation, DeviceBrandDto>(deviceBrand);

            if (errors.Any())
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, GetUnprocessableEntityResponse(HttpContext, errors));
            }

            var result = await _deviceService.UpdateBrandAsync(id, deviceBrand.Brand);

            if (result == null)
            {
                return NotFound(NotFoundMessage(id));
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] Guid id)
        {
            var result = await _deviceService.DeleteByIdAsync(id);

            if (result == null)
            {
                return NotFound(NotFoundMessage(id));
            }

            return Ok(result);
        }

        private string NotFoundMessage(Guid id) => $"Device id: {id} not found";

        private Dictionary<string, string[]> GetValidationRequestErrors<T, D>(D request) where T : IValidator<D>
        {
            var validator = Activator.CreateInstance<T>();
            return validator.Validate(request)
                .Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage ).ToArray());
        }

        private UnprocessableEntityResponseDto GetUnprocessableEntityResponse(HttpContext context, Dictionary<string, string[]> errors)
        {
            return new UnprocessableEntityResponseDto(
            errors,
            context.TraceIdentifier);
        }
    } 
}
