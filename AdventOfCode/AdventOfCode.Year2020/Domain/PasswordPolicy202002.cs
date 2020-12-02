namespace AdventOfCode.Year2020.Domain
{
    public class PasswordPolicy202002
    {
        public PasswordPolicy202002(string policyString)
        {
            var basePolicy = policyString.Split(' ');
            Character = char.Parse(basePolicy[1]);
            var occurrences = basePolicy[0].Split('-');
            MinOccurrence = int.Parse(occurrences[0]);
            MaxOccurrence = int.Parse(occurrences[1]);
        }

        public char Character { get; set; }
        public int MinOccurrence { get; set; }
        public int MaxOccurrence { get; set; }
    }
}