using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace AdventOfCode.Year2023.Solutions;

public class Solution202311Part2 : ISolution
{
    private readonly ILogger<Solution202311Part2> _logger;
    private readonly IInputService _inputService;
    private readonly string resourceLocation = "Resources2023\\Day11.txt";
    public int Expansion { get; set; } = 1000000;

    public Solution202311Part2(ILogger<Solution202311Part2> logger, IInputService inputService)
    {
        _logger = logger;
        _inputService = inputService;
    }
    public string GetSolution()
    {
        var universe = _inputService.GetInput(resourceLocation).Select(x => x.ToCharArray().ToList()).ToList();

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

        galaxyLocations = ExpandGalaxies(universe, galaxyLocations);
        // PrintGalaxies(galaxyLocations);

        var pairs = new Dictionary<string, (Point, Point)>();
        foreach (var startPoint in galaxyLocations)
        {
            foreach (var destinationPoint in galaxyLocations)
            {
                if (startPoint.id != destinationPoint.id)
                {
                    var key = $"{Math.Min(startPoint.id, destinationPoint.id)}-{Math.Max(startPoint.id, destinationPoint.id)}";
                    if (!pairs.ContainsKey(key))
                    {
                        pairs.Add(key, (startPoint.location, destinationPoint.location));
                    }
                }
            }
        }

        var distance = pairs.Sum(x => CalculateDistance(x.Value.Item1, x.Value.Item2));
        return $"{distance}";
    }

    static long CalculateDistance(Point point1, Point point2)
    {
        var deltaX = Math.Abs(point2.X - point1.X);
        var deltaY = Math.Abs(point2.Y - point1.Y);

        return deltaX + deltaY;
    }

    private List<(int id, Point location)> ExpandGalaxies(List<List<char>> universe, List<(int id, Point location)> galaxies)
    {
        var expandY = new List<int>();
        for (var rowIndex = 0; rowIndex < universe.Count; rowIndex++)
        {
            if (!universe[rowIndex].Any(x => x.Equals('#'))) expandY.Add(rowIndex);
        }

        var expandX = new List<int>();
        for (var colIndex = 0; colIndex < universe[0].Count; colIndex++)
        {
            if (!universe.Select(x => x[colIndex]).Any(x => x.Equals('#'))) expandX.Add(colIndex);
        }

        var result = new List<(int id, Point location)>();
        for (var i = 0; i < galaxies.Count; i++)
        {
            var galaxy = galaxies.ElementAt(i);
            var xFactor = expandX.Count(x => x < galaxy.location.X);
            var yFactor = expandY.Count(x => x < galaxy.location.Y);
            var locationX = galaxy.location.X + (xFactor * (Expansion - 1));
            var locationY = galaxy.location.Y + (yFactor * (Expansion - 1));
            result.Add((galaxy.id, new Point(locationX, locationY)));
        }

        return result;
    }

    public void PrintGalaxies(List<(int id, Point location)> galaxies)
    {
        foreach (var galaxy in galaxies)
        {
            _logger.LogInformation($"{galaxy.id} : {galaxy.location.X} - {galaxy.location.Y} (x,y)");
        }
    }
}
