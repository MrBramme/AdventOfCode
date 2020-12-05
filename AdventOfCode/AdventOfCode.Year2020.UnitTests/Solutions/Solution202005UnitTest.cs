using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202005UnitTest
    {
        private Solution202005 _sut;
        private Mock<ILogger<Solution202005>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202005>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202005(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenMultiple_ReturnsHighestId()
        {
            // Given
            var expected = "820";
            var input = new[] { "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }

        [TestCase("BFFFBBFRRR", "567")]
        [TestCase("FFFBBBFRRR", "119")]
        [TestCase("BBFFBBFRLL", "820")]
        public void GivenSingle_ReturnsSeatId(string input, string expected)
        {
            // Given
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(new[] { input });

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}