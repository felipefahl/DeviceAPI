using DeviceAPI.Dtos;
using FluentValidation;

namespace DeviceAPI.Validations
{
    public class DeviceDtoValidation : AbstractValidator<DeviceDto>
    {
        public DeviceDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(2, 60);

            RuleFor(x => x.Brand)
                .NotEmpty()
                .Length(2, 60);
        } 
    }
}
