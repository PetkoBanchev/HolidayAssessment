namespace HolidayAssessment.Tests
{
    using FluentAssertions;
    using HolidayAssessment.Services;
    using HolidayAssessment.Validators;
    using Moq;
    using Xunit;

    public class CountryValidatorTests
    {
        [Theory]
        [InlineData("NL")]
        [InlineData("nl")]
        [InlineData("Nl")]
        [InlineData("nL")]
        public async Task ValidateCountryCode_Should_NotThrow_WhenCodeIsValid(string input)
        {
            var mockCache = new Mock<ICountryCacheService>();

            mockCache.Setup(x => x.GetCountryCodesAsync()).ReturnsAsync(new List<string> { "NL", "DE", "FR" });

            var validator = new CountryValidator(mockCache.Object);

            Func<Task> act = async () =>
                await validator.ValidateCountryCodeAsync(input);

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ValidateCountryCode_ShouldThrow_WhenCodeIsInvalid()
        {
            var mockCache = new Mock<ICountryCacheService>();

            mockCache.Setup(x => x.GetCountryCodesAsync()).ReturnsAsync(new List<string> { "NL", "DE", "FR" });

            var validator = new CountryValidator(mockCache.Object);

            Func<Task> act = async () =>
                await validator.ValidateCountryCodeAsync("XX");

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Invalid country code*");
        }
    }
}
