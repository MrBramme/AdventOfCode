using AdventOfCode.Year2020.Domain;
using System.Linq;

namespace AdventOfCode.Year2020.Core
{
    public class PasswordValidator202002
    {
        public bool IsValid(string input)
        {
            var passwordParts = input.Split(':');
            var policy = new PasswordPolicy202002(passwordParts[0]);
            var passwordArray = passwordParts[1].ToCharArray();

            var actualOccurrences = passwordArray.Count(p => p.Equals(policy.Character));
            return actualOccurrences >= policy.MinOccurrence && actualOccurrences <= policy.MaxOccurrence;
        }
    }
}
