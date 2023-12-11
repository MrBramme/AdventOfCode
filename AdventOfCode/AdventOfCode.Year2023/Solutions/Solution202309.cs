using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Year2023.Solutions;

public class Solution202309 : ISolution
{
    private readonly ILogger<Solution202309> _logger;
    private readonly IInputService _inputService;
    private readonly string resourceLocation = "Resources2023\\Day09.txt";

    public Solution202309(ILogger<Solution202309> logger, IInputService inputService)
    {
        _logger = logger;
        _inputService = inputService;
    }
    public string GetSolution()
    {
        var values = _inputService.GetInput(resourceLocation).Select(ExtractNumbers);
        var result = new List<long>();

        foreach (var readings in values)
        {
            result.Add(ExtrapolateRecursively(readings));
        }

        // 2006998230 - too high
        return $"{result.Sum()}";
    }

    private long ExtrapolateRecursively(long[] numbers)
    {
        if (numbers.Any())
        {
            return ExtrapolateRecursively(GetNextRow(numbers)) + numbers.Last();
        }

        return 0;
    }
    private static long[] GetNextRow(long[] numbers)
    {
        return numbers.Zip(numbers.Skip(1)).Select(p => p.Second - p.First).ToArray();
    }

    private long[] ExtractNumbers(string input)
    {
        // opted for no regex due to negative numbers
        return input.Split(' ').Select(long.Parse).ToArray();
    }
}
