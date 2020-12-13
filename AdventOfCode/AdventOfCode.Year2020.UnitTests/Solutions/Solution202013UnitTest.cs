using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202013UnitTest
    {
        private Solution202013 _sut;
        private Mock<ILogger<Solution202013>> _loggerMock;
        private Mock<IInputService> _inputService;
        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202013>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202013(_loggerMock.Object, _inputService.Object);
        }
        [Test]
        public void GivenExample_ReturnsExpected()
        {
            // Given
            var expected = "295";
            var input = new[] { "939", "7,13,x,x,59,x,31,19" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);
            // When
            var result = _sut.GetSolution();
            // Then
            result.Should().Be(expected);
        }
    }
}