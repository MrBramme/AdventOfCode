using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202015UnitTest
    {
        private Solution202015 _sut;
        private Mock<ILogger<Solution202015>> _loggerMock;
        private Mock<IInputService> _inputService;
        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202015>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202015(_loggerMock.Object, _inputService.Object);
        }

        [TestCase("1,3,2", "1")]
        [TestCase("2,1,3", "10")]
        [TestCase("1,2,3", "27")]
        [TestCase("2,3,1", "78")]
        [TestCase("3,2,1", "438")]
        [TestCase("3,1,2", "1836")]
        public void GivenExample_ReturnsExpected(string inputRow, string expected)
        {
            // Given
            var input = new[] { inputRow };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);
            // When
            var result = _sut.GetSolution();
            // Then
            result.Should().Be(expected);
        }
    }
}