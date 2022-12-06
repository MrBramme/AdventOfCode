using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2022.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2022.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202206Part2UnitTest
    {
        private Solution202206Part2 _sut;
        private Mock<ILogger<Solution202206Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202206Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202206Part2(_loggerMock.Object, _inputService.Object);
        }

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", "19")]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", "23")]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", "23")]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", "29")]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", "26")]
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

