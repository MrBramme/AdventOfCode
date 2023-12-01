using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202301Part2UnitTest
    {
        private Solution202301Part2 _sut;
        private Mock<ILogger<Solution202301Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202301Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202301Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "281";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("two1nine\r\neightwothree\r\nabcone2threexyz\r\nxtwone3four\r\n4nineeightseven2\r\nzoneight234\r\n7pqrstsixteen".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

