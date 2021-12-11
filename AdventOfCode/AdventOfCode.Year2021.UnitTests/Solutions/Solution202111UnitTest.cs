using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202111UnitTest
    {
        private Solution202111 _sut;
        private Mock<ILogger<Solution202111>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202111>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202111(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "1656";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("5483143223;2745854711;5264556173;6141336146;6357385478;4167524645;2176841721;6882881134;4846848554;5283751526".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}