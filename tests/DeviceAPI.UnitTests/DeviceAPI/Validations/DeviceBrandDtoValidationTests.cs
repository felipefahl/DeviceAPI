using DeviceAPI.Dtos;
using DeviceAPI.Validations;
using FluentValidation.TestHelper;

namespace DeviceAPI.UnitTests.DeviceAPI.Validations
{
    public  class DeviceBrandDtoValidationTests
    {
        private readonly DeviceBrandDtoValidation _validator = new DeviceBrandDtoValidation();

        [Fact]
        public void GivenAnValidDevice_ShouldNotHaveValidationError()
        {
            var model = new DeviceBrandDto { Brand = "Xerox"};

            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(model => model.Brand);
        }

        [Fact]
        public void GivenAnOneLetterBrand_ShouldHaveValidationError()
        {
            var model = new DeviceBrandDto { Brand = "X" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Brand);
        }

        [Fact]
        public void GivenAnNullBrand_ShouldHaveValidationError()
        {
            var model = new DeviceBrandDto { };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Brand);
        }
    }
}
