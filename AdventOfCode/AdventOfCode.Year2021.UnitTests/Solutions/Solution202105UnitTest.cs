using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202105UnitTest
    {
        private Solution202105 _sut;
        private Mock<ILogger<Solution202105>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202105>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202105(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "5";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("0,9 -> 5,9;8,0 -> 0,8;9,4 -> 3,4;2,2 -> 2,1;7,0 -> 7,4;6,4 -> 2,0;0,9 -> 2,9;3,4 -> 1,4;0,0 -> 8,8;5,5 -> 8,2".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}