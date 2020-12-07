using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202007UnitTest
    {
        private Solution202007 _sut;
        private Mock<ILogger<Solution202007>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202007>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202007(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenMultiple_ReturnsExpectedSum()
        {
            // Given
            var expected = "4";
            var input = new[] { "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags." };
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input);

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}