using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202104UnitTest
    {
        private Solution202104 _sut;
        private Mock<ILogger<Solution202104>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202104>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202104(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "4512";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1;;22 13 17 11  0; 8  2 23  4 24;21  9 14 16  7; 6 10  3 18  5; 1 12 20 15 19;; 3 15  0  2 22; 9 18 13 17  5;19  8  7 25 23;20 11 10 24  4;14 21 16 12  6;;14 21 17 24  4;10 16 15  9 19;18  8 23 26 20;22 11 13  6  5; 2  0 12  3  7".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}