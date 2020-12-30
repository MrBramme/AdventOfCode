using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202020 : ISolution
    {
        private readonly ILogger<Solution202020> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day20.txt";
        private static readonly Regex _tileNameRegex = new Regex("^Tile (\\d*):$");
        private static readonly int _tileSize = 10;
        private List<Tile> _tiles = new List<Tile>();

        public Solution202020(ILogger<Solution202020> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);

            for (var i = 0; i < inputData.Length; i += _tileSize + 2)
            {
                _tiles.Add(new Tile(inputData[i], inputData.Skip(i + 1).Take(_tileSize).ToArray()));
            }

            long result = 1;

            foreach (var tile in _tiles)
            {
                tile.LeftTile = FindTileForBorder(tile, Border.Left);
                tile.RightTile = FindTileForBorder(tile, Border.Right);
                tile.TopTile = FindTileForBorder(tile, Border.Top);
                tile.BottomTile = FindTileForBorder(tile, Border.Bottom);

                if (tile.GetEdgeCount == 2)
                {
                    result *= tile.TileId;
                }
            }

            return $"{result}";
        }

        private Tile FindTileForBorder(Tile tile, Border border)
        {
            foreach (var currentTile in _tiles.Where(t => t.TileId != tile.TileId))
            {
                for (var rotations = 0; rotations < 4; rotations++)
                {
                    for (var flips = 0; flips < 2; flips++)
                    {
                        switch (border)
                        {
                            case Border.Left:
                                if (tile.GetBorder(Border.Left) == currentTile.GetBorder(Border.Right))
                                {
                                    return currentTile;
                                }
                                break;
                            case Border.Right:
                                if (tile.GetBorder(Border.Right) == currentTile.GetBorder(Border.Left))
                                {
                                    return currentTile;
                                }
                                break;
                            case Border.Top:
                                if (tile.GetBorder(Border.Bottom) == currentTile.GetBorder(Border.Top))
                                {
                                    return currentTile;
                                }
                                break;
                            case Border.Bottom:
                                if (tile.GetBorder(Border.Top) == currentTile.GetBorder(Border.Bottom))
                                {
                                    return currentTile;
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(border), border, null);
                        }
                        currentTile.Flip();
                    }
                    currentTile.Rotate();
                }
            }
            return null;
        }

        enum Border
        {
            Left = 0,
            Right = 1,
            Top = 2,
            Bottom = 3
        }

        class Tile
        {
            private char[][] _tileData;
            public readonly int TileId;

            public Tile LeftTile { get; set; }
            public Tile RightTile { get; set; }
            public Tile TopTile { get; set; }
            public Tile BottomTile { get; set; }

            public int GetEdgeCount => (LeftTile == null ? 0 : 1) + (RightTile == null ? 0 : 1) + (TopTile == null ? 0 : 1) + (BottomTile == null ? 0 : 1);

            public Tile(string tileName, string[] tileData)
            {
                _tileData = tileData.Select(row => row.ToCharArray()).ToArray();
                TileId = int.Parse(_tileNameRegex.Match(tileName).Groups[1].Value);
            }

            public void Rotate()
            {
                var rotatedTileData = new char[_tileSize][];

                for (var i = 0; i < _tileSize; ++i)
                {
                    rotatedTileData[i] = new char[_tileSize];
                    for (var j = 0; j < _tileSize; ++j)
                    {
                        rotatedTileData[i][j] = _tileData[_tileSize - j - 1][i];
                    }
                }

                _tileData = rotatedTileData;
            }

            public void Flip()
            {
                var flippedTileData = new char[_tileSize][];

                for (var i = 0; i < _tileSize; i++)
                {
                    flippedTileData[i] = new char[_tileSize];
                    for (var j = 0; j < _tileSize; j++)
                    {
                        flippedTileData[i][j] = _tileData[i][_tileSize - j - 1];
                    }
                }

                _tileData = flippedTileData;
            }

            public string GetBorder(Border border)
            {
                var sb = new StringBuilder();

                switch (border)
                {
                    case Border.Left:
                        for (var row = 0; row < _tileSize; row++)
                        {
                            sb.Append(_tileData[row][0]);
                        }
                        break;
                    case Border.Right:
                        for (var row = 0; row < _tileSize; row++)
                        {
                            sb.Append(_tileData[row][_tileSize - 1]);
                        }
                        break;
                    case Border.Top:
                        for (var col = 0; col < _tileSize; col++)
                        {
                            sb.Append(_tileData[0][col]);
                        }
                        break;
                    case Border.Bottom:
                        for (var col = 0; col < _tileSize; col++)
                        {
                            sb.Append(_tileData[_tileSize - 1][col]);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(border), border, null);
                }

                return sb.ToString();
            }
        }

    }
}