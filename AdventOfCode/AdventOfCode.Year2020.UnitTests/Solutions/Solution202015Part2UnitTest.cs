using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202015Part2UnitTest
    {
        private Solution202015Part2 _sut;
        private Mock<ILogger<Solution202015Part2>> _loggerMock;
        private Mock<IInputService> _inputService;
        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202015Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202015Part2(_loggerMock.Object, _inputService.Object);
        }

        [TestCase("0,3,6", "175594")]
        [TestCase("1,3,2", "2578")]
        [TestCase("2,1,3", "3544142")]
        [TestCase("1,2,3", "261214")]
        [TestCase("2,3,1", "6895259")]
        [TestCase("3,2,1", "18")]
        [TestCase("3,1,2", "362")]
        public void GivenExample_ReturnsExpected(string inputRow, string expected)
        {
            // Given
            var input = new[] { inputRow };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);
            // When
            var result = _sut.GetSolution();
            // Then
            result.Should().Be(expected);
        }
    }
}