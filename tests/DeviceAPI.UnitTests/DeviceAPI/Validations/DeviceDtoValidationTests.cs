using DeviceAPI.Dtos;
using DeviceAPI.Validations;
using FluentValidation.TestHelper;

namespace DeviceAPI.UnitTests.DeviceAPI.Validations
{
    public class DeviceDtoValidationTests
    {
        private readonly DeviceDtoValidation _validator = new DeviceDtoValidation();

        [Fact]
        public void GivenAnValidDevice_ShouldNotHaveValidationError()
        {
            var model = new DeviceDto { Name = "S23" , Brand = "Samsung"};

            var testValidator = _validator.TestValidate(model);

            testValidator.ShouldNotHaveValidationErrorFor(model => model.Name);
            testValidator.ShouldNotHaveValidationErrorFor(model => model.Brand);
        }

        [Fact]
        public void GivenAnOneLetterName_ShouldHaveValidationError()
        {
            var model = new DeviceDto { Name = "S", Brand = "Samsung" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Fact]
        public void GivenAnOneLetterBrand_ShouldHaveValidationError()
        {
            var model = new DeviceDto { Name = "S23", Brand = "S" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Brand);
        }

        [Fact]
        public void GivenAnNullName_ShouldHaveValidationError()
        {
            var model = new DeviceDto { Brand = "Samsung" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Fact]
        public void GivenAnNullBrand_ShouldHaveValidationError()
        {
            var model = new DeviceDto { Name = "S23" };

            _validator.TestValidate(model).ShouldHaveValidationErrorFor(model => model.Brand);
        }
    }
}
