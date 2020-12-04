using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020.Domain
{
    public class Passport202004
    {
        private readonly Dictionary<string, string> _passportFields = new Dictionary<string, string>();
        private readonly List<string> _requiredProperties = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        public Passport202004(string input)
        {
            var keyValueStrings = input.Split(' ');
            foreach (var pair in keyValueStrings)
            {
                var parts = pair.Split(':');
                _passportFields.Add(parts[0], parts[1]);
            }
        }

        public Dictionary<string, string> PassportFields => _passportFields;

        public bool RequiredFieldsPresent()
        {
            return _requiredProperties.All(prop => _passportFields.ContainsKey(prop));
        }
    }
}
