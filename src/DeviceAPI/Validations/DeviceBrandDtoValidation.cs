using DeviceAPI.Dtos;
using FluentValidation;

namespace DeviceAPI.Validations
{
    public class DeviceBrandDtoValidation : AbstractValidator<DeviceBrandDto>
    {
        public DeviceBrandDtoValidation()
        {
            RuleFor(x => x.Brand)
                .NotEmpty()
                .Length(2, 60);
        }
    }
}
