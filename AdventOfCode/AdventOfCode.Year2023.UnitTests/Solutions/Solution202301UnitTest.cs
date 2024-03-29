﻿using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2023.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdventOfCode.Year2023.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202301UnitTest
    {
        private Solution202301 _sut;
        private Mock<ILogger<Solution202301>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202301>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202301(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "142";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("1abc2\r\npqr3stu8vwx\r\na1b2c3d4e5f\r\ntreb7uchet".Split("\r\n"));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}

