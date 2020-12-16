using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202016Part2UnitTest
    {
        private Solution202016Part2 _sut;
        private Mock<ILogger<Solution202016Part2>> _loggerMock;
        private Mock<IInputService> _inputService;
        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202016Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202016Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExample_ReturnsExpected()
        {
            // Given
            var expected = 12 * 13;
            var input = new[] {
                "departure class: 0-1 or 4-19",
                "row: 0-5 or 8-19",
                "departure seat: 0-13 or 16-19",
                "",
                "your ticket:",
                "11,12,13",
                "",
                "nearby tickets:",
                "3,9,18",
                "15,1,5",
                "5,14,9" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be($"{expected}");
        }
    }
}