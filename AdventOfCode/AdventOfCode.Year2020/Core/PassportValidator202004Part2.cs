using AdventOfCode.Year2020.Domain;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020.Core
{
    public static class PassportValidator202004Part2
    {
        public static bool IsPassportValid(Passport202004 passport)
        {
            if (passport.RequiredFieldsPresent())
            {
                return IsBirthYearValid(passport.PassportFields["byr"]) &&
                       IsIssueYearValid(passport.PassportFields["iyr"]) &&
                       IsExpirationYearValid(passport.PassportFields["eyr"]) &&
                       IsHeightValid(passport.PassportFields["hgt"]) &&
                       IsHairColorValid(passport.PassportFields["hcl"]) &&
                       IsEyeColorValid(passport.PassportFields["ecl"]) &&
                       IsPassportIdValid(passport.PassportFields["pid"]);
            }

            return false;
        }

        public static bool IsBirthYearValid(string birthYear)
        {
            if (int.TryParse(birthYear, out var year))
            {
                return year >= 1920 && year <= 2002;
            }

            return false;
        }

        public static bool IsIssueYearValid(string issueYear)
        {
            if (int.TryParse(issueYear, out var year))
            {
                return year >= 2010 && year <= 2020;
            }

            return false;
        }

        public static bool IsExpirationYearValid(string expirationYear)
        {
            if (int.TryParse(expirationYear, out var year))
            {
                return year >= 2020 && year <= 2030;
            }

            return false;
        }

        public static bool IsHeightValid(string heightInput)
        {
            var heightString = heightInput.Substring(0, heightInput.Length - 2);
            var unit = heightInput.Substring(heightInput.Length - 2);
            if (int.TryParse(heightString, out var height))
            {
                if (unit.Equals("cm"))
                {
                    return height >= 150 && height <= 193;
                }
                if (unit.Equals("in"))
                {
                    return height >= 59 && height <= 76;
                }
            }

            return false;
        }

        public static bool IsHairColorValid(string hairColorInput)
        {
            // a # followed by exactly six characters 0-9 or a-f
            var regex = new Regex(@"^#([a-f]|[0-9]){6}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(hairColorInput);
        }

        public static bool IsEyeColorValid(string hairColorInput)
        {
            var validColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return validColors.Any(x => x.Equals(hairColorInput));
        }

        public static bool IsPassportIdValid(string passportId)
        {
            var trimmedId = passportId.Trim();
            if (trimmedId.Length == 9)
            {
                return int.TryParse(trimmedId, out _);
            }

            return false;
        }
    }
}
