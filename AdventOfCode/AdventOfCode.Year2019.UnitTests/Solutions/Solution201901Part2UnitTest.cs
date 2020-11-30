using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2019.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2019.UnitTests.Solutions
{
    [TestFixture]
    public class Solution201901Part2UnitTest
    {
        private Solution201901Part2 _sut;
        private Mock<ILogger<Solution201901Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution201901Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution201901Part2(_loggerMock.Object, _inputService.Object);
        }

        [TestCase("12", "2")]
        [TestCase("1969", "966")]
        [TestCase("100756", "50346")]
        public void GivenExampleInput_ReturnsExpected(string input, string expected)
        {
            // Given
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(new[] { input });

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }

        [Test]
        public void GivenMultipleInput_ReturnsExpected()
        {
            // Given
            var expected = "51314";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("12,1969,100756".Split(','));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}