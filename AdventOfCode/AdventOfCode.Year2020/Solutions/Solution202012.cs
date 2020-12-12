using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202012 : ISolution
    {
        private readonly ILogger<Solution202012> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day12.txt";
        public Solution202012(ILogger<Solution202012> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            var ship = new Ship();
            foreach (var movement in inputData)
            {
                var direction = GetDirection(movement.Substring(0, 1));
                var distance = int.Parse(movement.Substring(1));
                ship.Move(direction, distance);
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
                FacingDirection = Direction.East;
            }
            public int Horizontal { get; set; }
            public int Vertical { get; set; }
            public Direction FacingDirection { get; set; }

            public void Move(Direction direction, int distance)
            {
                switch (direction)
                {
                    case Direction.North:
                        Vertical += distance;
                        break;
                    case Direction.East:
                        Horizontal += distance;
                        break;
                    case Direction.South:
                        Vertical -= distance;
                        break;
                    case Direction.West:
                        Horizontal -= distance;
                        break;
                    case Direction.Front:
                        Move(FacingDirection, distance);
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
                    FacingDirection = FacingDirection switch
                    {
                        Direction.North => Direction.East,
                        Direction.East => Direction.South,
                        Direction.South => Direction.West,
                        Direction.West => Direction.North,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            }

            private void RotateLeft(int degree)
            {
                var steps = degree / 90;
                for (var step = 0; step < steps; step++)
                {
                    FacingDirection = FacingDirection switch
                    {
                        Direction.North => Direction.West,
                        Direction.East => Direction.North,
                        Direction.South => Direction.East,
                        Direction.West => Direction.South,
                        _ => throw new ArgumentOutOfRangeException()
                    };
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