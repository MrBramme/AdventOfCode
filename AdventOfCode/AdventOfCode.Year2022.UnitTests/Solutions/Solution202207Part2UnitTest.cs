using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2022.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2022.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202207Part2UnitTest
    {
        private Solution202207Part2 _sut;
        private Mock<ILogger<Solution202207Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202207Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202207Part2(_loggerMock.Object, _inputService.Object);
        }

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "7")]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", "5")]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", "6")]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "10")]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "11")]
        public void GivenExampleInput_ReturnsExpected(string input, string expected)
        {
            // Given
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(new[] { input });

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

