using EMS.Infrastructure.Helpers;
using EMS.Infrastructure.RequestModels;
using EMS.Infrastructure.Validator;

namespace EMS.Infrastructure.Tests
{
    public class UpdateEmployeeValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void UpdateEmployeeRequest_ShouldHave_ValidationError_Given_Name_ThatIs_NullEmptyOrWhitespace(string name)
        {
            //Arrange
            var sut = GetTestInstance();
            var request = new UpdateEmployeeRequest
            {
               Name = name,
               Email = "test@test.com"
            };

            //Act & Assert
            var result = sut.Validate(request);

            Assert.NotEmpty(result.Errors);
            Assert.Contains(result.Errors, x => x.PropertyName == "Name");
        }

        [Fact]
        public void UpdateEmployeeRequest_ShouldHave_ValidationError_Given_Name_ThatIs_More_Than_250_Chars()
        {
            //Arrange
            int charLength = 300;
            var sut = GetTestInstance();
            var request = new UpdateEmployeeRequest
            {
                Name = StringHelper.CreateString(charLength),
                Email = "test@test.com"
            };

            //Act & Assert
            var result = sut.Validate(request);

            Assert.NotEmpty(result.Errors);
            Assert.Contains(result.Errors, x => x.PropertyName == "Name");
            Assert.Equal($"'Name' must be between 1 and 250 characters. You entered {charLength} characters.", result.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void UpdateEmployeeRequest_ShouldHave_ValidationError_Given_Email_ThatIs_NullEmptyOrWhitespace(string email)
        {
            //Arrange
            var sut = GetTestInstance();
            var request = new UpdateEmployeeRequest
            {
                Name = "Test Employee",
                Email = email
            };

            //Act & Assert
            var result = sut.Validate(request);

            Assert.NotEmpty(result.Errors);
            Assert.Contains(result.Errors, x => x.PropertyName == "Email");
        }

        [Fact]
        public void UpdateEmployeeRequest_ShouldHave_ValidationError_Given_Email_ThatIs_More_Than_250_Chars()
        {
            //Arrange
            int charLength = 300;
            var sut = GetTestInstance();
            var request = new UpdateEmployeeRequest
            {
                Name = "Test Employee",
                Email = StringHelper.CreateString(charLength)
            };

            //Act & Assert
            var result = sut.Validate(request);

            Assert.NotEmpty(result.Errors);
            Assert.Contains(result.Errors, x => x.PropertyName == "Email");
            Assert.Equal($"'Email' must be between 1 and 250 characters. You entered {charLength} characters.", result.Errors[0].ErrorMessage);
        }


        private UpdateEmployeeValidator GetTestInstance()
        {
            return new UpdateEmployeeValidator();
        }

        
    }
}