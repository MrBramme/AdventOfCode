using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202114UnitTest
    {
        private Solution202114 _sut;
        private Mock<ILogger<Solution202114>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202114>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202114(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "1588";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("NNCB;;CH -> B;HH -> N;CB -> H;NH -> C;HB -> C;HC -> B;HN -> C;NN -> C;BH -> H;NC -> B;NB -> B;BN -> B;BB -> N;BC -> B;CC -> N;CN -> C".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}