using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace AdventOfCode.Year2023.Solutions;

public class Solution202311 : ISolution
{
    private readonly ILogger<Solution202311> _logger;
    private readonly IInputService _inputService;
    private readonly string resourceLocation = "Resources2023\\Day11.txt";

    public Solution202311(ILogger<Solution202311> logger, IInputService inputService)
    {
        _logger = logger;
        _inputService = inputService;
    }
    public string GetSolution()
    {
        var universe = _inputService.GetInput(resourceLocation).Select(x => x.ToCharArray().ToList()).ToList();

        ExpandUniverse(universe);
        PrintUniverse(universe);

        var galaxyLocations = new List<(int id, Point location)>();
        var idCounter = 0;
        for (var y = 0; y < universe.Count; y++)
        {
            for (var x = 0; x < universe[0].Count; x++)
            {
                if (universe[y][x] == '#')
                {
                    galaxyLocations.Add((idCounter, new Point(x, y)));
                    idCounter++;
                }
            }
        }

        var pairs = new Dictionary<string, (Point, Point)>();
        foreach (var startPoint in galaxyLocations)
        {
            foreach (var destinationPoint in galaxyLocations)
            {
                var key = $"{Math.Min(startPoint.id, destinationPoint.id)}-{Math.Max(startPoint.id, destinationPoint.id)}";
                if (!pairs.ContainsKey(key))
                {
                    pairs.Add(key, (startPoint.location, destinationPoint.location));
                }
            }
        }

        var distance = pairs.Sum(x => CalculateDistance(x.Value.Item1, x.Value.Item2));
        return $"{distance}";
    }

    static int CalculateDistance(Point point1, Point point2)
    {
        var deltaX = Math.Abs(point2.X - point1.X);
        var deltaY = Math.Abs(point2.Y - point1.Y);

        return deltaX + deltaY;
    }

    private static void ExpandUniverse(List<List<char>> universe)
    {
        var indicesForRowsToAdd = new List<int>();
        for (var rowIndex = 0; rowIndex < universe.Count; rowIndex++)
        {
            if (!universe[rowIndex].Any(x => x.Equals('#'))) indicesForRowsToAdd.Add(rowIndex);
        }

        var indicesForColumnsToAdd = new List<int>();
        for (var colIndex = 0; colIndex < universe[0].Count; colIndex++)
        {
            if (!universe.Select(x => x[colIndex]).Any(x => x.Equals('#'))) indicesForColumnsToAdd.Add(colIndex);
        }

        foreach (var row in universe)
        {
            foreach (var colIndex in indicesForColumnsToAdd.OrderDescending())
            {
                row.Insert(colIndex, '.');
            }
        }

        var emptyRow = Enumerable.Range(0, universe[0].Count).Select(_ => '.').ToList();
        foreach (var rowIndex in indicesForRowsToAdd.OrderDescending())
        {
            universe.Insert(rowIndex, emptyRow);
        }
    }

    public void PrintUniverse(IEnumerable<IEnumerable<char>> universe)
    {
        foreach (var row in universe)
        {
            _logger.LogInformation($"{string.Concat(row)}");
        }
    }
}
