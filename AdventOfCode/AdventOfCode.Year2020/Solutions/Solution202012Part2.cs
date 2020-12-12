using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202012Part2 : ISolution
    {
        private readonly ILogger<Solution202012Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day12.txt";
        public Solution202012Part2(ILogger<Solution202012Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            var waypoint = new Waypoint();
            var ship = new Ship();
            foreach (var movement in inputData)
            {
                var direction = GetDirection(movement.Substring(0, 1));
                var distance = int.Parse(movement.Substring(1));
                if (direction.Equals(Direction.North) ||
                    direction.Equals(Direction.East) ||
                    direction.Equals(Direction.South) ||
                    direction.Equals(Direction.West) ||
                    direction.Equals(Direction.Left) ||
                    direction.Equals(Direction.Right))
                {
                    waypoint.Move(direction, distance);
                }
                else
                {
                    if (direction.Equals(Direction.Front))
                    {
                        var horizontalDistance = waypoint.Horizontal * distance;
                        var verticalDistance = waypoint.Vertical * distance;
                        ship.Horizontal += horizontalDistance;
                        ship.Vertical += verticalDistance;
                    }
                }

            }
            var result = Math.Abs(ship.Horizontal) + Math.Abs(ship.Vertical);
            return $"{result}";
        }

        private Direction GetDirection(string cmd)
        {
            return cmd switch
            {
                "N" => Direction.North,
                "E" => Direction.East,
                "S" => Direction.South,
                "W" => Direction.West,
                "F" => Direction.Front,
                "L" => Direction.Left,
                "R" => Direction.Right,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        class Ship
        {
            public Ship()
            {
                Horizontal = 0;
                Vertical = 0;
            }
            public int Horizontal { get; set; }
            public int Vertical { get; set; }
        }

        class Waypoint
        {
            public Waypoint()
            {
                North = 1;
                East = 10;
                South = 0;
                West = 0;
                FacingDirection = Direction.East;
            }
            public int North { get; set; }
            public int East { get; set; }
            public int South { get; set; }
            public int West { get; set; }
            public int Horizontal => East - West;
            public int Vertical => North - South;
            public Direction FacingDirection { get; set; }

            public void Move(Direction direction, int distance)
            {
                switch (direction)
                {
                    case Direction.North:
                        North += distance;
                        break;
                    case Direction.East:
                        East += distance;
                        break;
                    case Direction.South:
                        South += distance;
                        break;
                    case Direction.West:
                        West += distance;
                        break;
                    case Direction.Left:
                        RotateLeft(distance);
                        break;
                    case Direction.Right:
                        RotateRight(distance);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }

            private void RotateRight(int degree)
            {
                var steps = degree / 90;
                for (var step = 0; step < steps; step++)
                {
                    (var n, var e, var s, var w) = (North, East, South, West);
                    North = w;
                    East = n;
                    South = e;
                    West = s;
                }
            }

            private void RotateLeft(int degree)
            {
                var steps = degree / 90;
                for (var step = 0; step < steps; step++)
                {
                    (var n, var e, var s, var w) = (North, East, South, West);
                    North = e;
                    East = s;
                    South = w;
                    West = n;
                }
            }
        }

        enum Direction
        {
            North,
            East,
            South,
            West,
            Front,
            Left,
            Right
        }
    }
}