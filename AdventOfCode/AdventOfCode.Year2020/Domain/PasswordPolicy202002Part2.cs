using System.Linq;

namespace AdventOfCode.Year2020.Domain
{
    public class PasswordPolicy202002Part2
    {
        public PasswordPolicy202002Part2(string policyString)
        {
            var basePolicy = policyString.Split(' ');
            Character = char.Parse(basePolicy[1]);
            Indices = basePolicy[0].
                Split('-')
                .Select(i => int.Parse(i) - 1)
                .ToArray();
        }

        public char Character { get; set; }
        public int[] Indices { get; set; }
    }
}