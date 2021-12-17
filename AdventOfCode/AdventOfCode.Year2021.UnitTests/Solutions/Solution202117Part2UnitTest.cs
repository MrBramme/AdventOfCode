using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202117Part2UnitTest
    {
        private Solution202117Part2 _sut;
        private Mock<ILogger<Solution202117Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202117Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202117Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "112";
            var input = "target area: x=20..30, y=-10..-5";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input.Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}