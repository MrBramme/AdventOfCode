using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions;

[TestFixture]
public class Solution202309UnitTest
{
    private Solution202309 _sut;
    private Mock<ILogger<Solution202309>> _loggerMock;
    private Mock<IInputService> _inputService;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<Solution202309>>();
        _inputService = new Mock<IInputService>();
        _sut = new Solution202309(_loggerMock.Object, _inputService.Object);
    }

    [Test]
    public void GivenExampleInput_ReturnsExpected()
    {
        // Given
        var expected = "114";
        _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45".Split("\r\n"));

        // When
        var result = _sut.GetSolution();

        // Then
        result.Should().Be(expected);
    }
}
