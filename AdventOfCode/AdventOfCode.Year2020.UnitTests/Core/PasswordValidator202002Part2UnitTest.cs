using AdventOfCode.Year2020.Core;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Core
{
    [TestFixture]
    public class PasswordValidator202002Part2UnitTest
    {
        private PasswordValidator202002Part2 _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new PasswordValidator202002Part2();
        }

        [TestCase("1-3 a: abcde", true)]
        [TestCase("1-3 b: cdefg", false)]
        [TestCase("2-9 c: ccccccccc", false)]
        public void IsValid_GivenInput_ReturnsExpected(string input, bool expected)
        {
            // Given When
            var result = _sut.IsValid(input);

            // Then
            result.Should().Be(expected);
        }
    }
}