using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023.Solutions
{
    public class Solution202305Part2 : ISolution
    {
        private readonly ILogger<Solution202305Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2023\\Day05.txt";

        public Solution202305Part2(ILogger<Solution202305Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var values = _inputService.GetInput(resourceLocation);
            var seedRangeValues = ExtractNumbers(values[0]);

            var seedRanges = new List<SeedRange>();
            for (var i = 0; i < seedRangeValues.Length; i = i + 2)
            {
                seedRanges.Add(new SeedRange(seedRangeValues[i], seedRangeValues[i] + seedRangeValues[i + 1] - 1));
            }

            var mapping = new NumberMapping();
            for (var i = 3; i < values.Length; i++)
            {
                var numbers = ExtractNumbers(values[i]);
                if (numbers.Length == 0)
                {
                    if (string.IsNullOrEmpty(values[i]))
                    {
                        seedRanges = ProcessMapping(seedRanges, mapping);
                        mapping = new NumberMapping();
                        Console.WriteLine($"{string.Concat(seedRanges.Select(x => $"{x.Start}-{x.End} | "))}");
                    }
                    else
                    {
                        _logger.LogInformation(values[i]);
                    }
                    continue;
                }

                mapping.AddMapping(numbers[0], numbers[1], numbers[2]);

            }

            seedRanges = ProcessMapping(seedRanges, mapping);
            Console.WriteLine($"{string.Concat(seedRanges.Select(x => $"{x.Start}-{x.End} | "))}");
            var result = seedRanges.Min(x => x.Start);
            if (result <= 23536990) { _logger.LogError("too low"); }

            throw new NotImplementedException("This is not working yet");
            return $"{result}";
        }

        private static List<SeedRange> ProcessMapping(List<SeedRange> seedRanges, NumberMapping mapping)
        {
            var newSeedRanges = new List<SeedRange>();
            foreach (var seedRange in seedRanges)
            {
                newSeedRanges.AddRange(mapping.GetNewSeedRange(seedRange));
            }

            return newSeedRanges;
        }

        private long[] ExtractNumbers(string input)
        {
            var matches = Regex.Matches(input, @"\d+");
            return matches.Select(x => long.Parse(x.Value)).ToArray();
        }

        private class NumberMapping
        {
            private readonly List<(long start, long end, long destinationStart, long destinationEnd)> _ranges = new();
            public void AddMapping(long destination, long source, long rangeLength)
            {
                (long sourceStart, long sourceEnd, long destinationStart, long destinationEnd) range = (source, source + rangeLength - 1, destination, destination + rangeLength - 1);
                _ranges.Add(range);
            }

            public List<SeedRange> GetNewSeedRange(SeedRange input)
            {
                var result = new List<SeedRange>();

                if (_ranges.Any(x => input.Start >= x.start && input.End <= x.end)) // move all
                {
                    var range = _ranges.Last(x => input.Start >= x.start && input.End <= x.end);
                    // Console.WriteLine($"MID: Input is: {input.Start}-{input.End} + Source is: {range.sourceStart}-{range.sourceEnd} (dest: {range.destinationStart})");
                    var startOffset = input.Start - range.start;
                    var newSeedRange = new SeedRange(range.destinationStart + startOffset, range.destinationEnd - range.end + input.End);
                    result.Add(newSeedRange);
                    return result;
                }

                if (_ranges.Any(x => input.Start < x.start && input.End > x.end)) // split in 3
                {
                    var range = _ranges.Last(x => input.Start < x.start && input.End > x.end);
                    // Console.WriteLine($"BITE: Input is: {input.Start}-{input.End} + Source is: {range.sourceStart}-{range.sourceEnd} (dest: {range.destinationStart})");
                    result.Add(new SeedRange(range.end + 1, input.End));
                    result.Add(new SeedRange(input.Start, range.start - 1));
                    var newSeedRange = new SeedRange(range.destinationStart, range.destinationEnd);
                    //Console.WriteLine($"BITE: Split 1: {range.sourceEnd + 1}-{input.End} (end)");
                    //Console.WriteLine($"BITE: Split 2: {input.Start}-{range.sourceStart - 1} (start)");
                    //Console.WriteLine($"BITE: Split 3: {newSeedRange.Start}-{newSeedRange.End} (offset: {offset})");
                    result.Add(newSeedRange);
                    return result;
                }

                if (_ranges.Any(x => input.End < x.end && input.End > x.start && input.Start < x.start)) // right overlap
                {
                    var range = _ranges.Last(x => input.End < x.end && input.End > x.start && input.Start < x.start);
                    //Console.WriteLine($"RIGHT: Input is: {input.Start}-{input.End} + Source is: {range.sourceStart}-{range.sourceEnd} (dest: {range.destinationStart})");
                    result.Add(new SeedRange(input.Start, range.start - 1));
                    var newSeedRange = new SeedRange(range.destinationStart, range.destinationStart + input.End - range.start);
                    result.Add(newSeedRange);
                    return result;
                }

                if (_ranges.Any(x => input.Start > x.start && input.Start < x.end && input.End > x.end)) // left overlap
                {
                    var range = _ranges.Last(x => input.Start > x.start && input.Start < x.end && input.End > x.end);
                    //Console.WriteLine($"LEFT: Input is: {input.Start}-{input.End} + Source is: {range.sourceStart}-{range.sourceEnd} (dest: {range.destinationStart})");
                    //Console.WriteLine($"LEFT: Split 1: {range.sourceEnd + 1}-{input.End}");
                    result.Add(new SeedRange(range.end + 1, input.End));
                    var offset = range.destinationStart - range.start;
                    var newSeedRange = new SeedRange(input.Start + offset, range.end + offset);
                    // Console.WriteLine($"LEFT: Split 2: {newSeedRange.Start}-{newSeedRange.End} (offset: {offset})");
                    result.Add(newSeedRange);
                    return result;
                }
                Console.WriteLine($"NO CHANGE: Input is: {input.Start}-{input.End} + ranges are: {string.Concat(_ranges.Select(x => $"{x.start}-{x.end} |"))}");
                result.Add(input); // no change
                return result;
            }
        }

        private class SeedRange
        {
            public SeedRange(long start, long end)
            {
                Start = start;
                End = end;
            }
            public long Start { get; }
            public long End { get; }
        }
    }
}