using AdventOfCode.Year2020.Core;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Year2020.UnitTests.Core
{
    [TestFixture]
    public class PassportValidator202004Part2UnitTest
    {
        [TestCase("2002", true)]
        [TestCase("2003", false)]
        [TestCase("1920", true)]
        [TestCase("1919", false)]
        [TestCase("1", false)]
        [TestCase("20021", false)]
        [TestCase("test", false)]
        public void IsBirthYearValid_GivenInput_ShouldReturnExpected(string input, bool expected)
        {
            // Given When
            var result = PassportValidator202004Part2.IsBirthYearValid(input);

            // Then
            result.Should().Be(expected);
        }

        [TestCase("2009", false)]
        [TestCase("2010", true)]
        [TestCase("2020", true)]
        [TestCase("2021", false)]
        [TestCase("1", false)]
        [TestCase("20021", false)]
        [TestCase("test", false)]
        public void IsIssueYearValid_GivenInput_ShouldReturnExpected(string input, bool expected)
        {
            // Given When
            var result = PassportValidator202004Part2.IsIssueYearValid(input);

            // Then
            result.Should().Be(expected);
        }

        [TestCase("2019", false)]
        [TestCase("2020", true)]
        [TestCase("2030", true)]
        [TestCase("2031", false)]
        [TestCase("1", false)]
        [TestCase("20021", false)]
        [TestCase("test", false)]
        public void IsExpirationYearValid_GivenInput_ShouldReturnExpected(string input, bool expected)
        {
            // Given When
            var result = PassportValidator202004Part2.IsExpirationYearValid(input);

            // Then
            result.Should().Be(expected);
        }

        [TestCase("60in", true)]
        [TestCase("190cm", true)]
        [TestCase("190in", false)]
        [TestCase("190", false)]
        public void IsHeightValid_GivenInput_ShouldReturnExpected(string input, bool expected)
        {
            // Given When
            var result = PassportValidator202004Part2.IsHeightValid(input);

            // Then
            result.Should().Be(expected);
        }

        [TestCase("#123abc", true)]
        [TestCase("#aaaaaa", true)]
        [TestCase("#aaaaaaa", false)]
        [TestCase("#123abz", false)]
        [TestCase("123abc", false)]
        public void IsHairColorValid_GivenInput_ShouldReturnExpected(string input, bool expected)
        {
            // Given When
            var result = PassportValidator202004Part2.IsHairColorValid(input);

            // Then
            result.Should().Be(expected);
        }

        [TestCase("amb", true)]
        [TestCase("blu", true)]
        [TestCase("brn", true)]
        [TestCase("gry", true)]
        [TestCase("grn", true)]
        [TestCase("hzl", true)]
        [TestCase("oth", true)]
        [TestCase("wat", false)]
        public void IsEyeColorValid_GivenInput_ShouldReturnExpected(string input, bool expected)
        {
            // Given When
            var result = PassportValidator202004Part2.IsEyeColorValid(input);

            // Then
            result.Should().Be(expected);
        }

        [TestCase("000000001", true)]
        [TestCase("0123456789", false)]
        public void IsPassportIdValid_GivenInput_ShouldReturnExpected(string input, bool expected)
        {
            // Given When
            var result = PassportValidator202004Part2.IsPassportIdValid(input);

            // Then
            result.Should().Be(expected);
        }
    }
}