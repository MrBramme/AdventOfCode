using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2022.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2022.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202208UnitTest
    {
        private Solution202208 _sut;
        private Mock<ILogger<Solution202208>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202208>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202208(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "21";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("30373\r\n25512\r\n65332\r\n33549\r\n35390".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

