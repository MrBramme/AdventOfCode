using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202002Part2UnitTest
    {
        private Solution202002Part2 _sut;
        private Mock<ILogger<Solution202002Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202002Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202002Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "1";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(new[] { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" });

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}