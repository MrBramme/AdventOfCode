using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202112UnitTest
    {
        private Solution202112 _sut;
        private Mock<ILogger<Solution202112>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202112>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202112(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "10";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("start-A;start-b;A-c;A-b;b-d;A-end;b-end".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }

        [Test]
        public void GivenLargerExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "19";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("dc-end;HN-start;start-kj;dc-start;dc-HN;LN-dc;HN-end;kj-sa;kj-HN;kj-dc".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }

        [Test]
        public void GivenLargestExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "226";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("fs-end;he-DX;fs-he;start-DX;pj-DX;end-zg;zg-sl;zg-pj;pj-he;RW-he;fs-DX;pj-RW;zg-RW;start-pj;he-WI;zg-he;pj-fs;start-RW".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}