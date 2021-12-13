using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2021.Solutions
{
    public class Solution202113Part2 : ISolution
    {
        private readonly ILogger<Solution202113Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2021\\Day13.txt";

        public Solution202113Part2(ILogger<Solution202113Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation);
            var grid = GetDotsGrid(input);
            var foldInstructions = input
                .SkipWhile(i => !string.IsNullOrEmpty(i))
                .Skip(1)
                .Select(x =>
                {
                    var d = x.Remove(0, 11).Split('=');
                    return new FoldInstruction(d[0] == "y", int.Parse(d[1]));
                });

            foreach(var foldInstruction in foldInstructions)
            {
                if (foldInstruction.IsHorizontal)
                {
                    var newGrid = Enumerable.Range(0, foldInstruction.Location).Select(r => grid[r]).ToArray();
                    var maxX = newGrid[0].Length;
                    for (int i = 0; i < foldInstruction.Location; i++)
                    {
                        var y = grid.Length - i - 1;
                        for(var x = 0; x < maxX; x++)
                        {
                            if (grid[y][x])
                            {
                                newGrid[i][x] = true;
                            }
                        }
                    }
                    grid = newGrid;
                } else
                {
                    var newGrid = Enumerable.Range(0, grid.Length).Select(r => grid[r][0..(foldInstruction.Location)]).ToArray();
                    var maxY = newGrid.Length;
                    var maxX = newGrid[0].Length;
                    for (int y = 0; y < maxY; y++)
                    {
                        for (var i = 0; i < maxX; i++)
                        {
                            var x = grid[0].Length - i - 1;
                            if (grid[y][x])
                            {
                                newGrid[y][i] = true;
                            }
                        }
                    }
                    grid = newGrid;
                }
            }
            
            return $"{GetCode(grid)}";
        }

        public bool[][] GetDotsGrid(string[] input)
        {
            var dotsData = input.TakeWhile(i => !string.IsNullOrEmpty(i)).Select(y => y.Split(',').Select(int.Parse).ToArray()).ToArray();
            var maxX = dotsData.Select(x => x[0]).Max();
            var maxY = dotsData.Select(x => x[1]).Max();
            var grid = Enumerable.Range(0, maxY + 1).Select(r => new bool[maxX + 1]).ToArray();
            foreach(var row in dotsData)
            {
                grid[row[1]][row[0]] = true;
            }
            return grid;
        }

        public record FoldInstruction(bool IsHorizontal, int Location);

        public string GetCode(bool[][] grid)
        {
            var strWriter = new StringWriter();
            strWriter.WriteLine();
            foreach (var row in grid)
            {
                foreach(var col in row)
                {
                    strWriter.Write(col ? "#" : ".");
                }
                strWriter.WriteLine();
            }
            return strWriter.ToString();
        }
    }
}