using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;

namespace AdventOfCode.Year2023.UnitTests.Solutions;

[TestFixture]
public class Solution202311UnitTest
{
    private Solution202311 _sut;
    private Mock<ILogger<Solution202311>> _loggerMock;
    private Mock<IInputService> _inputService;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<Solution202311>>();
        _inputService = new Mock<IInputService>();
        _sut = new Solution202311(_loggerMock.Object, _inputService.Object);
    }

    [Test]
    public void GivenExampleInput_ReturnsExpected()
    {
        // Given
        var expected = "374";
        _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("...#......\r\n.......#..\r\n#.........\r\n..........\r\n......#...\r\n.#........\r\n.........#\r\n..........\r\n.......#..\r\n#...#.....".Split("\r\n"));

        // When
        var result = _sut.GetSolution();

        // Then
        result.Should().Be(expected);
    }
}
