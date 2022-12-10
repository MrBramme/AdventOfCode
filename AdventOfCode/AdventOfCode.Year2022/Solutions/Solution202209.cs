using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202209 : ISolution
    {
        private readonly ILogger<Solution202209> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day09.txt";

        public Solution202209(ILogger<Solution202209> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).Select(x => x.Split(" ")).Select(y => (direction: GetDirection(y[0]), moves: int.Parse(y[1]))).ToList();

            var tailPosition = new Point(0, 0);
            var headPosition = new Point(0, 0);
            var knownPositions = new List<Point>
            {
                tailPosition
            };

            foreach (var movement in input)
            {
                for (var i = 0; i < movement.moves; i++)
                {
                    headPosition = MoveHead(headPosition, movement.direction);
                    if (NeedToMoveTail(headPosition, tailPosition))
                    {
                        tailPosition = MoveTail(tailPosition, headPosition);
                        knownPositions.Add(tailPosition);
                    }
                }
            }
            var result = knownPositions.Distinct().Count();
            return $"{result}";
        }

        private Point MoveTail(Point tail, Point head)
        {
            if (tail.X == head.X)
            {
                if (tail.Y > head.Y)
                {
                    return new Point(tail.X, tail.Y - 1);
                }
                return new Point(tail.X, tail.Y + 1);
            }

            if (tail.Y == head.Y)
            {
                if (tail.X > head.X)
                {
                    return new Point(tail.X - 1, tail.Y);
                }
                return new Point(tail.X + 1, tail.Y);
            }

            if (tail.X > head.X)
            {
                if (tail.Y > head.Y)
                {
                    return new Point(tail.X - 1, tail.Y - 1);
                }
                return new Point(tail.X - 1, tail.Y + 1);
            }

            if (tail.X < head.X)
            {
                if (tail.Y > head.Y)
                {
                    return new Point(tail.X + 1, tail.Y - 1);
                }
                return new Point(tail.X + 1, tail.Y + 1);
            }

            throw new ArgumentOutOfRangeException();
        }

        private Point MoveHead(Point point, Direction direction)
        {
            return direction switch
            {
                Direction.Up => new Point(point.X, point.Y + 1),
                Direction.Down => new Point(point.X, point.Y - 1),
                Direction.Left => new Point(point.X - 1, point.Y),
                Direction.Right => new Point(point.X + 1, point.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        private bool NeedToMoveTail(Point head, Point tail)
        {
            return Math.Abs(head.X - tail.X) > 1 || Math.Abs(head.Y - tail.Y) > 1;
        }

        private Direction GetDirection(string input)
        {
            return input.ToLower() switch
            {
                "u" => Direction.Up,
                "d" => Direction.Down,
                "l" => Direction.Left,
                "r" => Direction.Right,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}