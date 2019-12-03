using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Domain;

namespace AdventOfCode._2019
{
    public class Day03 : Solution
    {
        public override void Run(bool test = false)
        {
            var input = GetDemoInput();
            if (!test)
            {
                input = File.ReadAllText("Data/2019/03.txt");
            }
            var (line1, line2) = ParseInput(input);

            Console.WriteLine("Step 1");
            var currentPosition = new Coordinate(0, 0);
            var line1Coordinates = new Dictionary<string, Coordinate>();
            var steps = 0;
            foreach (var movement in line1)
            {
                var (coordinates, endPosition) = GetCoordinates(movement, currentPosition);
                foreach (var coordinate in coordinates)
                {
                    steps++;
                    if (!line1Coordinates.ContainsKey(coordinate.pos))
                    {
                        coordinate.Steps = steps;
                        line1Coordinates.Add(coordinate.pos, coordinate);
                    }
                }
                currentPosition = endPosition;
            }

            Console.WriteLine("Step 2");
            currentPosition = new Coordinate(0, 0);
            var minDist = int.MaxValue;
            var minCrossing = int.MaxValue;
            steps = 0;
            foreach (var movement in line2)
            {
                var (coordinates, endPosition) = GetCoordinates(movement, currentPosition);
                foreach (var coordinate in coordinates)
                {
                    steps++;
                    if (line1Coordinates.ContainsKey(coordinate.pos))
                    {
                        if (steps + line1Coordinates[coordinate.pos].Steps < minCrossing)
                        {
                            minCrossing = steps + line1Coordinates[coordinate.pos].Steps;
                        }
                        var dist = Math.Abs(coordinate.X) + Math.Abs(coordinate.Y);
                        if(dist < minDist)
                        {
                            minDist = dist;
                        }
                    }
                }
                //line2Coordinates.AddRange(coordinates);
                currentPosition = endPosition;
            }

            Console.WriteLine("Step 3: result");
            //var crossings = line1Coordinates.Where(x => line2Coordinates.Contains(x)).Select(y => Math.Abs(y.X) + Math.Abs(y.Y)).ToList();

            Console.WriteLine($"Min Distance: {minDist}");
            Console.WriteLine($"Min Crossing Distance: {minCrossing}");

        }

        private string GetDemoInput(bool first = true)
        {
            return first ?
                "R75,D30,R83,U83,L12,D49,R71,U7,L72\r\nU62,R66,U55,R34,D71,R55,D58,R83" : // 159 + 610
                "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\r\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7"; // 135 + 410
        }

        private (string[], string[]) ParseInput(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return (lines[0].Split(','), lines[1].Split(','));
        }

        private (List<Coordinate>, Coordinate) GetCoordinates(string input, Coordinate startPosition)
        {
            var result = new List<Coordinate>();
            var dirString = input.Substring(0, 1);
            var distance = int.Parse(input.Substring(1));
            var currentPos = startPosition;
            for(var i = 0; i < distance; i++)
            {
                Coordinate newPos;
                switch (dirString) {
                    case "U":
                        newPos = new Coordinate(currentPos.X, currentPos.Y + 1);
                        break;
                    case "D":
                        newPos = new Coordinate(currentPos.X, currentPos.Y - 1);
                        break;
                    case "L":
                        newPos = new Coordinate(currentPos.X - 1, currentPos.Y);
                        break;
                    case "R":
                        newPos = new Coordinate(currentPos.X + 1, currentPos.Y);
                        break;
                    default:
                        throw new Exception();
                }
                result.Add(newPos);
                currentPos = newPos;
            }
            return (result, currentPos);
        }

        private class Coordinate
        {
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Steps { get; set; }

            public string pos => $"{X}-{Y}";

        }
    }
    
}
