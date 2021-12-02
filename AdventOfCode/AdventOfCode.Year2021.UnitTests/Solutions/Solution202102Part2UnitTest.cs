using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202102Part2UnitTest
    {
        private Solution202102Part2 _sut;
        private Mock<ILogger<Solution202102Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202102Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202102Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "900";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("forward 5,down 5,forward 8,up 3,down 8,forward 2".Split(','));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}