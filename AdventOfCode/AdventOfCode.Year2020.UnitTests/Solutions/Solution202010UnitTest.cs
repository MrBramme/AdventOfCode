using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202010UnitTest
    {
        private Solution202010 _sut;
        private Mock<ILogger<Solution202010>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202010>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202010(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExample1_ReturnsExpected()
        {
            // Given
            var expected = "35";
            var input = new[] { "16", "10", "15", "5", "1", "11", "7", "19", "6", "12", "4" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }

        [Test]
        public void GivenExample2_ReturnsExpected()
        {
            // Given
            var expected = "220";
            var input = new[] { "28", "33", "18", "42", "31", "14", "46", "20", "48", "47", "24", "23", "49", "45", "19", "38", "39", "11", "1", "32", "25", "35", "8", "17", "7", "9", "4", "2", "34", "10", "3" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}