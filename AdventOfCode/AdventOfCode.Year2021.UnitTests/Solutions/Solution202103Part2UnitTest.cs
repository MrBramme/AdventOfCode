using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202103Part2UnitTest
    {
        private Solution202103Part2 _sut;
        private Mock<ILogger<Solution202103Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202103Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202103Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "230";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("00100,11110,10110,10111,10101,01111,00111,11100,10000,11001,00010,01010".Split(','));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}