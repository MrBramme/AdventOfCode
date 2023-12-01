using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202302UnitTest
    {
        private Solution202302 _sut;
        private Mock<ILogger<Solution202302>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202302>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202302(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

