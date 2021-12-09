using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202109Part2UnitTest
    {
        private Solution202109Part2 _sut;
        private Mock<ILogger<Solution202109Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202109Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202109Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "1134";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("2199943210;3987894921;9856789892;8767896789;9899965678".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}