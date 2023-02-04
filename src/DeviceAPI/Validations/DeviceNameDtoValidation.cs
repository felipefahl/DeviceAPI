using DeviceAPI.Dtos;
using FluentValidation;

namespace DeviceAPI.Validations
{
    public class DeviceNameDtoValidation : AbstractValidator<DeviceNameDto>
    {
        public DeviceNameDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(2, 60);
        }
    }
}
