using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2022.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2022.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202201Part2UnitTest
    {
        private Solution202201Part2 _sut;
        private Mock<ILogger<Solution202201Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202201Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202201Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "45000";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("1000,2000,3000,,4000,,5000,6000,,7000,8000,9000,,10000".Split(','));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

