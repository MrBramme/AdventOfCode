using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202305UnitTest
    {
        private Solution202305 _sut;
        private Mock<ILogger<Solution202305>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202305>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202305(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "35";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("seeds: 79 14 55 13\r\n\r\nseed-to-soil map:\r\n50 98 2\r\n52 50 48\r\n\r\nsoil-to-fertilizer map:\r\n0 15 37\r\n37 52 2\r\n39 0 15\r\n\r\nfertilizer-to-water map:\r\n49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4\r\n\r\nwater-to-light map:\r\n88 18 7\r\n18 25 70\r\n\r\nlight-to-temperature map:\r\n45 77 23\r\n81 45 19\r\n68 64 13\r\n\r\ntemperature-to-humidity map:\r\n0 69 1\r\n1 0 69\r\n\r\nhumidity-to-location map:\r\n60 56 37\r\n56 93 4".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

