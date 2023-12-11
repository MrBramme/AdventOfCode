using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions;

[TestFixture]
public class Solution202308Part2UnitTest
{
    private Solution202308Part2 _sut;
    private Mock<ILogger<Solution202308Part2>> _loggerMock;
    private Mock<IInputService> _inputService;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<Solution202308Part2>>();
        _inputService = new Mock<IInputService>();
        _sut = new Solution202308Part2(_loggerMock.Object, _inputService.Object);
    }

    [Test]
    public void GivenExampleInput_ReturnsExpected()
    {
        // Given
        var expected = "6";
        _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("LR\r\n\r\n11A = (11B, XXX)\r\n11B = (XXX, 11Z)\r\n11Z = (11B, XXX)\r\n22A = (22B, XXX)\r\n22B = (22C, 22C)\r\n22C = (22Z, 22Z)\r\n22Z = (22B, 22B)\r\nXXX = (XXX, XXX)".Split("\r\n"));

        // When
        var result = _sut.GetSolution();

        // Then
        result.Should().Be(expected);
    }
}
