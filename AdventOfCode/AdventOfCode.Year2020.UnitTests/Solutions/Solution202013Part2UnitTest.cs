using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202013Part2UnitTest
    {
        private Solution202013Part2 _sut;
        private Mock<ILogger<Solution202013Part2>> _loggerMock;
        private Mock<IInputService> _inputService;
        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202013Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202013Part2(_loggerMock.Object, _inputService.Object);
        }

        [TestCase("7,13,x,x,59,x,31,19", "1068781")]
        [TestCase("17,x,13,19", "3417")]
        [TestCase("67,7,59,61", "754018")]
        [TestCase("67,x,7,59,61", "779210")]
        [TestCase("67,7,x,59,61", "1261476")]
        [TestCase("1789,37,47,1889", "1202161486")]
        public void GivenExample_ReturnsExpected(string inputSchedule, string expected)
        {
            // Given
            var input = new[] { "ignored", inputSchedule };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);
            // When
            var result = _sut.GetSolution();
            // Then
            result.Should().Be(expected);
        }
    }
}