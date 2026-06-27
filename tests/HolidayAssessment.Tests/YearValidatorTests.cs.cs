namespace HolidayAssessment.Tests
{
    using HolidayAssessment.Validators;
    using Xunit;
    using FluentAssertions;

    public class YearValidatorTests
    {
        [Fact]
        public void Validate_Should_NotThrow_WhenYearIsValid()
        {
            var year = 2000;

            var action = () => YearValidator.Validate(year);

            action.Should().NotThrow();
        }

        [Fact]
        public void Validate_ShouldThrow_WhenYearIsTooSmall()
        {
            var year = 1975;

            var action = () => YearValidator.Validate(year);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Validate_ShouldThrow_WhenYearIsTooLarge()
        {
            var year = 2077;

            var action = () => YearValidator.Validate(year);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
