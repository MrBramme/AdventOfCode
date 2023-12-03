using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202303UnitTest
    {
        private Solution202303 _sut;
        private Mock<ILogger<Solution202303>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202303>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202303(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "4361";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("467..114..\r\n...*......\r\n..35..633.\r\n......#...\r\n617*......\r\n.....+.58.\r\n..592.....\r\n......755.\r\n...$.*....\r\n.664.598..".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

