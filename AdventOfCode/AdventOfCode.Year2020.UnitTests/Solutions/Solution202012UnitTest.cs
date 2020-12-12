using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202012UnitTest
    {
        private Solution202012 _sut;
        private Mock<ILogger<Solution202012>> _loggerMock;
        private Mock<IInputService> _inputService;
        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202012>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202012(_loggerMock.Object, _inputService.Object);
        }
        [Test]
        public void GivenExample_ReturnsExpected()
        {
            // Given
            var expected = "25";
            var input = new[] { "F10", "N3", "F7", "R90", "F11" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);
            // When
            var result = _sut.GetSolution();
            // Then
            result.Should().Be(expected);
        }
    }
}