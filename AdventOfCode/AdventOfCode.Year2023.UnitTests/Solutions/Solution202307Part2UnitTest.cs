using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202307Part2UnitTest
    {
        private Solution202307Part2 _sut;
        private Mock<ILogger<Solution202307Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202307Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202307Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "5905";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("32T3K 765\r\nT55J5 684\r\nKK677 28\r\nKTJJT 220\r\nQQQJA 483".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

