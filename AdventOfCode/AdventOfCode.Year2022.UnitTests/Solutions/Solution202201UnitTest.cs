using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2022.Solutions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AdventOfCode.Year2022.UnitTests.Solutions
{
    [TestFixture]
    public class Solution202201UnitTest
    {
        private Solution202201 _sut;
        private Mock<ILogger<Solution202201>> _loggerMock;
        private Mock<IInputService> _inputService;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<Solution202201>>();
            _inputService = new Mock<IInputService>();
            _sut = new Solution202201(_loggerMock.Object, _inputService.Object);
        }

        [Test]
        public void GivenExampleInput_ReturnsExpected()
        {
            // Given

            // When
            var result = _sut.GetSolution();

            // Then
           
        }
    }
}