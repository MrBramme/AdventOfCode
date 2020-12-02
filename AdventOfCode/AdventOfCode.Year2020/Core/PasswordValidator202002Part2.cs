using AdventOfCode.Year2020.Domain;

namespace AdventOfCode.Year2020.Core
{
    public class PasswordValidator202002Part2
    {
        public bool IsValid(string input)
        {
            var passwordParts = input.Split(':');
            var policy = new PasswordPolicy202002Part2(passwordParts[0]);
            var passwordArray = passwordParts[1].Trim().ToCharArray();

            var foundForPosition1 = passwordArray[policy.Indices[0]].Equals(policy.Character) ? 1 : 0;
            var foundForPosition2 = passwordArray[policy.Indices[1]].Equals(policy.Character) ? 1 : 0;
            return foundForPosition1 + foundForPosition2 == 1;
        }
    }
}
