using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions;

[TestFixture]
public class Solution202308UnitTest
{
    private Solution202308 _sut;
    private Mock<ILogger<Solution202308>> _loggerMock;
    private Mock<IInputService> _inputService;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<Solution202308>>();
        _inputService = new Mock<IInputService>();
        _sut = new Solution202308(_loggerMock.Object, _inputService.Object);
    }

    [Test]
    public void GivenExampleInput_ReturnsExpected()
    {
        // Given
        var expected = "2";
        _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("RL\r\n\r\nAAA = (BBB, CCC)\r\nBBB = (DDD, EEE)\r\nCCC = (ZZZ, GGG)\r\nDDD = (DDD, DDD)\r\nEEE = (EEE, EEE)\r\nGGG = (GGG, GGG)\r\nZZZ = (ZZZ, ZZZ)".Split("\r\n"));

        // When
        var result = _sut.GetSolution();

        // Then
        result.Should().Be(expected);
    }

    [Test]
    public void GivenExample2Input_ReturnsExpected()
    {
        // Given
        var expected = "6";
        _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("LLR\r\n\r\nAAA = (BBB, BBB)\r\nBBB = (AAA, ZZZ)\r\nZZZ = (ZZZ, ZZZ)".Split("\r\n"));

        // When
        var result = _sut.GetSolution();

        // Then
        result.Should().Be(expected);
    }
}
