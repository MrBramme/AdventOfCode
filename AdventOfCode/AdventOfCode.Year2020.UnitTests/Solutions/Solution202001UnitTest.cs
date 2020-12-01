﻿using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2020.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202001UnitTest
    {
        private Solution202001 _sut;
        private Mock<ILogger<Solution202001>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202001>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202001(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "514579";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("1721,979,366,299,675,1456".Split(','));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}