using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202113UnitTest
    {
        private Solution202113 _sut;
        private Mock<ILogger<Solution202113>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202113>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202113(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "17";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("6,10;0,14;9,10;0,3;10,4;4,11;6,0;6,12;4,1;0,13;10,12;3,4;3,0;8,4;1,10;2,14;8,10;9,0;;fold along y=7;fold along x=5".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}