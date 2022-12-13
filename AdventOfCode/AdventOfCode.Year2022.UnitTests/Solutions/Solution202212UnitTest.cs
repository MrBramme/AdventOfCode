using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2022.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2022.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202212UnitTest
    {
        private Solution202212 _sut;
        private Mock<ILogger<Solution202212>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202212>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202212(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "31";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("Sabqponm\r\nabcryxxl\r\naccszExk\r\nacctuvwj\r\nabdefghi".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

