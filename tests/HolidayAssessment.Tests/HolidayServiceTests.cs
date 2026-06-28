using HolidayAssessment.Models;
using HolidayAssessment.Repositories;
using HolidayAssessment.Services;
using Moq;
using FluentAssertions;
using HolidayAssessment.Clients;
using System.Diagnostics;

namespace HolidayAssessment.Tests
{
    public class HolidayServiceTests
    {
        #region GetLastThreeHolidays
        [Fact]
        public async Task GetLastThreeHolidays_Should_ReturnLatestThreePastHolidays_WhenInSingleYear()
        {
            var repository = new Mock<IHolidayRepository>();
            var client = new Mock<INagerApiClient>();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var currentYear = today.Year;

            repository.Setup(r => r.GetByCountryAndYearAsync("NL", currentYear))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { Name = "Holiday 5", Date = today.AddDays(+2) },
                    new() { Name = "Holiday 2", Date = today.AddDays(-2) },
                    new() { Name = "Holiday 1", Date = today.AddDays(-1) },
                    new() { Name = "Holiday 4", Date = today.AddDays(+1) },
                    new() { Name = "Holiday 3", Date = today.AddDays(-3) }
                });

            repository.Setup(r => r.GetByCountryAndYearAsync("NL", currentYear - 1))
                .ReturnsAsync(new List<Holiday>());

            var service = new HolidayService(client.Object, repository.Object);

            var result = await service.GetLastThreeHolidaysAsync("NL");

            result.Should().HaveCount(3);
            result[0].Name.Should().Be("Holiday 1");
            result[1].Name.Should().Be("Holiday 2");
            result[2].Name.Should().Be("Holiday 3");
        }

        [Fact]
        public async Task GetLastThreeHolidays_Should_ReturnLatestThreePastHolidays_WhenInTwoYears()
        {
            var repository = new Mock<IHolidayRepository>();
            var client = new Mock<INagerApiClient>();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var currentYear = today.Year;

            repository.Setup(r => r.GetByCountryAndYearAsync("NL", currentYear))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { Name = "Holiday 1", Date = today.AddDays(-1) },
                    new() { Name = "Holiday 5", Date = today.AddDays(+2) },
                    new() { Name = "Holiday 4", Date = today.AddDays(+1) }
                });

            repository.Setup(r => r.GetByCountryAndYearAsync("NL", currentYear - 1))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { Name = "Holiday 2", Date = today.AddDays(-2) },
                    new() { Name = "Holiday 3", Date = today.AddDays(-3) },
                    new() { Name = "Holiday 6", Date = today.AddDays(-4) }
                });

            var service = new HolidayService(client.Object, repository.Object);

            var result = await service.GetLastThreeHolidaysAsync("NL");

            result.Should().HaveCount(3);
            result[0].Name.Should().Be("Holiday 1");
            result[1].Name.Should().Be("Holiday 2");
            result[2].Name.Should().Be("Holiday 3");
        }

        [Fact]
        public async Task GetLastThreeHolidays_Should_ReturnEmptyList_WhenNoHolidaysExist()
        {
            var repository = new Mock<IHolidayRepository>();
            var client = new Mock<INagerApiClient>();

            var currentYear = DateTime.Today.Year;

            repository.Setup(r => r.GetByCountryAndYearAsync("NL", currentYear))
                .ReturnsAsync(new List<Holiday>());

            repository.Setup(r => r.GetByCountryAndYearAsync("NL", currentYear - 1))
                .ReturnsAsync(new List<Holiday>());

            var service = new HolidayService(client.Object, repository.Object);

            var result = await service.GetLastThreeHolidaysAsync("NL");

            result.Should().BeEmpty();
        }
        #endregion

        #region GetWeekdayHolidaysAsync
        [Fact]
        public async Task GetWeekdayHolidays_Should_RemoveWeekendHolidays()
        {
            var repository = new Mock<IHolidayRepository>();
            var client = new Mock<INagerApiClient>();

            var year = 2026;

            repository
                .Setup(r => r.GetByCountriesAndYearAsync(It.IsAny<List<string>>(), year))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { Name = "Monday", CountryCode = "NL", Date = new DateOnly(2026, 6, 15) },
                    new() { Name = "Saturday", CountryCode = "NL", Date = new DateOnly(2026, 6, 20) },
                    new() { Name = "Sunday", CountryCode = "NL", Date = new DateOnly(2026, 6, 21) }
                });

            var service = new HolidayService(client.Object, repository.Object);

            var result = await service.GetWeekdayHolidaysAsync(year, new List<string> { "NL" });

            result.Should().HaveCount(1);
            result[0].Name.Should().Be("Monday");
        }

        [Fact]
        public async Task GetWeekdayHolidays_Should_ReturnEmpty_WhenNoWeekdayHolidays()
        {
            var repository = new Mock<IHolidayRepository>();
            var client = new Mock<INagerApiClient>();

            var year = 2026;

            repository
                .Setup(r => r.GetByCountriesAndYearAsync(It.IsAny<List<string>>(), year))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { Name = "Saturday", CountryCode = "NL", Date = new DateOnly(2026, 6, 13) },
                    new() { Name = "Sunday", CountryCode = "NL", Date = new DateOnly(2026, 6, 14) },
                    new() { Name = "Saturday", CountryCode = "NL", Date = new DateOnly(2026, 6, 20) },
                    new() { Name = "Sunday", CountryCode = "NL", Date = new DateOnly(2026, 6, 21) }
                });

            var service = new HolidayService(client.Object, repository.Object);

            var result = await service.GetWeekdayHolidaysAsync(year, new List<string> { "NL" });

            result.Should().HaveCount(0);
        }

        #endregion

        #region GetWeekdayHolidaysCountAsync

        [Fact]
        public async Task GetWeekdayHolidaysCount_Should_ReturnWHCountPerCountry()
        {
            var repository = new Mock<IHolidayRepository>();
            var client = new Mock<INagerApiClient>();

            var year = 2026;

            repository
                .Setup(r => r.GetByCountriesAndYearAsync(It.IsAny<List<string>>(), year))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { Name = "Monday", CountryCode = "NL", Date = new DateOnly(2026, 6, 15) },
                    new() { Name = "Saturday", CountryCode = "NL", Date = new DateOnly(2026, 6, 20) },
                    new() { Name = "Sunday", CountryCode = "NL", Date = new DateOnly(2026, 6, 21) },
                    new() { Name = "Monday", CountryCode = "DE", Date = new DateOnly(2026, 6, 15) },
                    new() { Name = "Tuesday", CountryCode = "DE", Date = new DateOnly(2026, 6, 16) },
                    new() { Name = "Saturday", CountryCode = "DE", Date = new DateOnly(2026, 6, 20) },
                    new() { Name = "Sunday", CountryCode = "DE", Date = new DateOnly(2026, 6, 21) }
                });

            var service = new HolidayService(client.Object, repository.Object);

            var result = await service.GetWeekdayHolidaysCountAsync(year, new List<string> { "NL", "DE" });

            result.Should().HaveCount(2);
            result.Should().ContainSingle(x => x.CountryCode == "DE" && x.Count == 2);
            result.Should().ContainSingle(x => x.CountryCode == "NL" && x.Count == 1);
        }

        [Fact]
        public async Task GetWeekdayHolidaysCount_Should_ReturnWHCountPerCountryWhenOneCountryHasZeroWH()
        {
            var repository = new Mock<IHolidayRepository>();
            var client = new Mock<INagerApiClient>();

            var year = 2026;

            repository
                .Setup(r => r.GetByCountriesAndYearAsync(It.IsAny<List<string>>(), year))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { Name = "Saturday", CountryCode = "NL", Date = new DateOnly(2026, 6, 20) },
                    new() { Name = "Sunday", CountryCode = "NL", Date = new DateOnly(2026, 6, 21) },
                    new() { Name = "Monday", CountryCode = "DE", Date = new DateOnly(2026, 6, 15) },
                    new() { Name = "Tuesday", CountryCode = "DE", Date = new DateOnly(2026, 6, 16) },
                    new() { Name = "Saturday", CountryCode = "DE", Date = new DateOnly(2026, 6, 20) },
                    new() { Name = "Sunday", CountryCode = "DE", Date = new DateOnly(2026, 6, 21) }
                });

            var service = new HolidayService(client.Object, repository.Object);

            var result = await service.GetWeekdayHolidaysCountAsync(year, new List<string> { "NL", "DE" });

            result.Should().HaveCount(2);
            result.Should().ContainSingle(x => x.CountryCode == "DE" && x.Count == 2);
            result.Should().ContainSingle(x => x.CountryCode == "NL" && x.Count == 0);
        }

        #endregion

        #region GetNumberOfSharedHolidaysAsync
        [Fact]
        public async Task GetSharedHolidaysCount_Should_ReturnOnlySharedHolidays()
        {
            var repository = new Mock<IHolidayRepository>();
            var client = new Mock<INagerApiClient>();

            var year = 2026;

            repository
                .Setup(r => r.GetByCountryAndYearAsync("NL", year))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { LocalName = "NL_Monday", CountryCode = "NL", Date = new DateOnly(2026, 6, 15) },
                    new() { LocalName = "NL_Saturday", CountryCode = "NL", Date = new DateOnly(2026, 6, 20) },
                    new() { LocalName = "NL_Sunday", CountryCode = "NL", Date = new DateOnly(2026, 6, 28) }
                });

            repository
                .Setup(r => r.GetByCountryAndYearAsync("DE", year))
                .ReturnsAsync(new List<Holiday>
                {
                    new() { LocalName = "DE_Monday", CountryCode = "DE", Date = new DateOnly(2026, 6, 15) },
                    new() { LocalName = "DE_Tuesday", CountryCode = "DE", Date = new DateOnly(2026, 6, 16) },
                    new() { LocalName = "DE_Saturday", CountryCode = "DE", Date = new DateOnly(2026, 6, 20) },
                    new() { LocalName = "DE_Sunday", CountryCode = "DE", Date = new DateOnly(2026, 6, 21) }
                });

            var service = new HolidayService(client.Object, repository.Object);

            var result = await service.GetNumberOfSharedHolidaysAsync(year, "NL", "DE");

            result.Should().HaveCount(2);
            result.Should().ContainSingle(x => x.LocalNameA == "NL_Monday" && x.LocalNameB == "DE_Monday");
            result.Should().ContainSingle(x => x.LocalNameA == "NL_Saturday" && x.LocalNameB == "DE_Saturday");
        }
        #endregion
    }
}
