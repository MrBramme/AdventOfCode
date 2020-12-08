using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202008Part2UnitTest
    {
        private Solution202008Part2 _sut;
        private Mock<ILogger<Solution202008Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202008Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202008Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenMultiple_ReturnsExpectedSum()
        {
            // Given
            var expected = "8";
            var input = new[] { "nop +0", "acc +1", "jmp +4", "acc +3", "jmp -3", "acc -99", "acc +1", "jmp -4", "acc +6" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}