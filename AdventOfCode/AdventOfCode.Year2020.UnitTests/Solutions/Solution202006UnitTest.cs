using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202006UnitTest
    {
        private Solution202006 _sut;
        private Mock<ILogger<Solution202006>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202006>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202006(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenMultiple_ReturnsExpectedSum()
        {
            // Given
            var expected = "11";
            var input = new[] { "abc", "", "a", "b", "c", "", "ab", "ac", "", "a", "a", "a", "a", "", "b" };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}