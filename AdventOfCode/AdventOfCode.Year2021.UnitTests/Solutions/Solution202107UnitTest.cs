using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202107UnitTest
    {
        private Solution202107 _sut;
        private Mock<ILogger<Solution202107>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202107>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202107(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "37";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("16,1,2,0,4,2,7,1,2,14".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}