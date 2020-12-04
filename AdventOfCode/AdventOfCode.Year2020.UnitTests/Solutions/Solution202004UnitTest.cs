using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202004UnitTest
    {
        private Solution202004 _sut;
        private Mock<ILogger<Solution202004>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202004>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202004(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "2";
            var input = new[] { "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
                                "byr:1937 iyr:2017 cid:147 hgt:183cm",
                                "",
                                "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
                                "hcl:#cfa07d byr:1929",
                                "",
                                "hcl:#ae17e1 iyr:2013",
                                "eyr:2024",
                                "ecl:brn pid:760753108 byr:1931",
                                "hgt:179cm",
                                "",
                                "hcl:#cfa07d eyr:2025 pid:166559648",
                                "iyr:2011 ecl:brn hgt:59in"};
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}