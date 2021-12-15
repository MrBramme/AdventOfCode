using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202115UnitTest
    {
        private Solution202115 _sut;
        private Mock<ILogger<Solution202115>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202115>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202115(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "40";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("1163751742;1381373672;2136511328;3694931569;7463417111;1319128137;1359912421;3125421639;1293138521;2311944581".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}