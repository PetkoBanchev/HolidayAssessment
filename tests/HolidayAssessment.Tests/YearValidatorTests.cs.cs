namespace HolidayAssessment.Tests
{
    using HolidayAssessment.Validators;
    using Xunit;

    public class YearValidatorTests
    {
        [Fact]
        public void Validate_Should_NotThrow_WhenYearIsValid()
        {
            // Arrange
            var year = 2000;

            // Act
            var action = () => YearValidator.Validate(year);

            // Assert
            Assert.Null(Record.Exception(action));
        }

        [Fact]
        public void Validate_ShouldThrow_WhenYearIsTooSmall()
        {
            var year = 1975;

            Assert.Throws<ArgumentOutOfRangeException>(() => YearValidator.Validate(year));
        }

        [Fact]
        public void Validate_ShouldThrow_WhenYearIsTooLarge()
        {
            var year = 2077;

            Assert.Throws<ArgumentOutOfRangeException>(() => YearValidator.Validate(year));
        }
    }
}
