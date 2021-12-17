using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202116UnitTest
    {
        private Solution202116 _sut;
        private Mock<ILogger<Solution202116>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202116>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202116(_loggerMock.Object, _inputService.Object);
        }

        [TestCase("D2FE28", "2027")]
        [TestCase("38006F45291200", "31")]
        [TestCase("8A004A801A8002F478", "16")]
        [TestCase("620080001611562C8802118E34", "12")]
        [TestCase("C0015000016115A2E0802F182340", "23")]
        [TestCase("A0016C880162017C3686B18A3D4780", "31")]
        public void GivenExampleInput_ReturnsExpected(string input, string expected)
        {
            // Given
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns(input.Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}