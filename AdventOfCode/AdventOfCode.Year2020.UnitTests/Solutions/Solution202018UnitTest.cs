using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202018UnitTest
    {
        private Solution202018 _sut;
        private Mock<ILogger<Solution202018>> _loggerMock;
        private Mock<IInputService> _inputService;
        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202018>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202018(_loggerMock.Object, _inputService.Object);
        }

        [TestCase("1 + 2 * 3 + 4 * 5 + 6", "71")]
        [TestCase("1 + (2 * 3) + (4 * (5 + 6))", "51")]
        [TestCase("2 * 3 + (4 * 5)", "26")]
        [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)", "437")]
        [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", "12240")]
        [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "13632")]
        public void GivenSingleExample_ReturnsExpected(string inputRow, string expected)
        {
            // Given
            var input = new[] { inputRow };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }

        [Test]
        public void GivenMultipleExample_ReturnsExpected()
        {
            // Given
            var expected = $"{71 + 51 + 26}";
            var input = new[] { "1 + 2 * 3 + 4 * 5 + 6", "1 + (2 * 3) + (4 * (5 + 6))", "2 * 3 + (4 * 5)" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}