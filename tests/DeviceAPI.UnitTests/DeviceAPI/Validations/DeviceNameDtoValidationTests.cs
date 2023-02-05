using DeviceAPI.Dtos;
using DeviceAPI.Validations;
using FluentValidation.TestHelper;

namespace DeviceAPI.UnitTests.DeviceAPI.Validations
{
    public  class DeviceNameDtoValidationTests
    {
        private readonly DeviceNameDtoValidation _validator = new DeviceNameDtoValidation();

        [Fact]
        public void GivenAnValidDevice_ShouldNotHaveValidationError()
        {
            var model = new DeviceNameDto { Name = "S23"};

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(model => model.Name);
        }

        [Fact]
        public void GivenAnOneLetterName_ShouldHaveValidationError()
        {
            var model = new DeviceNameDto { Name = "S" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Fact]
        public void GivenAnNullName_ShouldHaveValidationError()
        {
            var model = new DeviceNameDto { };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }
    }
}
