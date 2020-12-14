using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202014Part2UnitTest
    {
        private Solution202014Part2 _sut;
        private Mock<ILogger<Solution202014Part2>> _loggerMock;
        private Mock<IInputService> _inputService;
        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202014Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202014Part2(_loggerMock.Object, _inputService.Object);
        }
        [Test]
        public void GivenExample_ReturnsExpected()
        {
            // Given
            var expected = "208";
            var input = new[] { "mask = 000000000000000000000000000000X1001X", "mem[42] = 100", "mask = 00000000000000000000000000000000X0XX", "mem[26] = 1" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);
            // When
            var result = _sut.GetSolution();
            // Then
            result.Should().Be(expected);
        }
    }
}