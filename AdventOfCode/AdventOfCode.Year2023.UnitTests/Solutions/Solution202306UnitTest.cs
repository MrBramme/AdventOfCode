using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202306UnitTest
    {
        private Solution202306 _sut;
        private Mock<ILogger<Solution202306>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202306>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202306(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "288";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("Time:      7  15   30\r\nDistance:  9  40  200".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

