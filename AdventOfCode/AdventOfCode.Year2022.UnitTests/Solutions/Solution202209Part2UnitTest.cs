﻿using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2022.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2022.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202209Part2UnitTest
    {
        private Solution202209Part2 _sut;
        private Mock<ILogger<Solution202209Part2>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202209Part2>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202209Part2(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "1";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }

        [Test]
        public void GivenLargerExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "36";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

