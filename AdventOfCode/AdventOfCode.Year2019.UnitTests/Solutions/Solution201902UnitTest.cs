using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2019.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2019.UnitTests.Solutions
{
    [TestFixture]
    public class Solution201902UnitTest
    {
        private Solution201902 _sut;
        private Mock<ILogger<Solution201902>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution201902>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution201902(_loggerMock.Object, _inputService.Object);
        }

        [TestCase(new[] { 1, 0, 0, 0, 99 }, new[] { 2, 0, 0, 0, 99 })]
        [TestCase(new[] { 2, 3, 0, 3, 99 }, new[] { 2, 3, 0, 6, 99 })]
        [TestCase(new[] { 2, 4, 4, 5, 99, 0 }, new[] { 2, 4, 4, 5, 99, 9801 })]
        [TestCase(new[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        public void GivenExampleInput_ReturnsExpected(int[] input, int[] expected)
        {
            // Given When
            var result = _sut.ProcessIntOp(input);

            // Then
            result.Should().BeEquivalentTo(expected);
        }
    }
}