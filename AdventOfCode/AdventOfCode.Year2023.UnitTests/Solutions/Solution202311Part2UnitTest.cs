using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;

namespace AdventOfCode.Year2023.UnitTests.Solutions;

[TestFixture]
public class Solution202311Part2UnitTest
{
    private Solution202311Part2 _sut;
    private Mock<ILogger<Solution202311Part2>> _loggerMock;
    private Mock<IInputService> _inputService;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<Solution202311Part2>>();
        _inputService = new Mock<IInputService>();
        _sut = new Solution202311Part2(_loggerMock.Object, _inputService.Object);
    }

    [Test]
    public void GivenExampleInput_ReturnsExpected()
    {
        // Given
        _sut.Expansion = 10;
        var expected = "1030";
        _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("...#......\r\n.......#..\r\n#.........\r\n..........\r\n......#...\r\n.#........\r\n.........#\r\n..........\r\n.......#..\r\n#...#.....".Split("\r\n"));

        // When
        var result = _sut.GetSolution();

        // Then
        result.Should().Be(expected);
    }

    [Test]
    public void GivenExampleInput2_ReturnsExpected()
    {
        // Given
        _sut.Expansion = 100;
        var expected = "8410";
        _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("...#......\r\n.......#..\r\n#.........\r\n..........\r\n......#...\r\n.#........\r\n.........#\r\n..........\r\n.......#..\r\n#...#.....".Split("\r\n"));

        // When
        var result = _sut.GetSolution();

        // Then
        result.Should().Be(expected);
    }
}
