﻿using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2021.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2021.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202110UnitTest
    {
        private Solution202110 _sut;
        private Mock<ILogger<Solution202110>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202110>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202110(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given
            var expected = "26397";
            _inputService.Setup(x => x.GetInput(It.IsAny<string>())).Returns("[({(<(())[]>[[{[]{<()<>>;[(()[<>])]({[<{<<[]>>(;{([(<{}[<>[]}>{[]{[(<()>;(((({<>}<{<{<>}{[]{[]{};[[<[([]))<([[{}[[()]]];[{[{({}]{}}([{[{{{}}([];{<[[]]>}<{[{[{[]{()[[[];[<(<(<(<{}))><([]([]();<{([([[(<>()){}]>(<<{{;<{([{{}}[<[[[<>{}]]]>[]]".Split(';'));

            // When
            var result = _sut.GetSolution();

            // Then
            result.Should().Be(expected);
        }
    }
}