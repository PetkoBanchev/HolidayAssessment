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


        [Theory]
        [InlineData(" ")]
        [InlineData("nl ")]
        [InlineData(" Nl")]
        [InlineData("n")]
        [InlineData("nll")]
        public async Task ValidateCountryCode_ShouldThrow_WhenCodeIsInvalid(string input)
        {
            var mockCache = new Mock<ICountryCacheService>();

            mockCache.Setup(x => x.GetCountryCodesAsync()).ReturnsAsync(new List<string> { "NL", "DE", "FR" });

            var validator = new CountryValidator(mockCache.Object);

            Func<Task> act = async () =>
                await validator.ValidateCountryCodeAsync(input);

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Invalid country code*");
        }

        [Theory]
        [MemberData(nameof(ValidCountryLists))]
        public async Task ValidateCountryCodes_Should_NotThrow_WhenCodeIsValid(List<string> input)
        {
            var mockCache = new Mock<ICountryCacheService>();

            mockCache.Setup(x => x.GetCountryCodesAsync()).ReturnsAsync(new List<string> { "NL", "DE", "FR" });

            var validator = new CountryValidator(mockCache.Object);

            Func<Task> act = async () =>
                await validator.ValidateCountryCodesAsync(input);

            await act.Should().NotThrowAsync();
        }

        [Theory]
        [MemberData(nameof(InvalidCountryLists))]
        public async Task ValidateCountryCodes_ShouldThrow_WhenCodeIsInvalid(List<string> input)
        {
            var mockCache = new Mock<ICountryCacheService>();

            mockCache.Setup(x => x.GetCountryCodesAsync()).ReturnsAsync(new List<string> { "NL", "DE", "FR" });

            var validator = new CountryValidator(mockCache.Object);

            Func<Task> act = async () =>
                await validator.ValidateCountryCodesAsync(input);

            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Invalid country code*");
        }

        public static IEnumerable<object[]> ValidCountryLists =>
            new[]
            {
                new object[] { new List<string> { "NL" } },
                new object[] { new List<string> { "nl" } },
                new object[] { new List<string> { "NL", "DE" } },
                new object[] { new List<string> { "nl", "de" } },
                new object[] { new List<string> { "NL", "DE", "fr" } },
                new object[] { new List<string> { "nl", "de", "FR" } },
                new object[] { new List<string> { "Nl", "dE", "fr" } }
            };
        public static IEnumerable<object[]> InvalidCountryLists =>
            new[]
            {
                new object[] { new List<string> { "N" } },
                new object[] { new List<string> { "nl " } },
                new object[] { new List<string> { "NL", "DEx" } },
                new object[] { new List<string> { "n l", "de" } },
                new object[] { new List<string> { " NL", "DE", "fr" } },
                new object[] { new List<string> { "  ", "de", "FR" } },
                new object[] { new List<string> { "Nls", "dEr", "frq" } }
            };
    }
}
