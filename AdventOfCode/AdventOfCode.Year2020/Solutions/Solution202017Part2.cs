using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Year2020.Solutions
{
    public class Solution202017Part2 : ISolution
    {
        private readonly ILogger<Solution202017Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2020\\Day17.txt";
        public Solution202017Part2(ILogger<Solution202017Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }
        public string GetSolution()
        {
            var inputData = _inputService.GetInput(resourceLocation);
            var cubes = ParseInputToDictionary(inputData);

            AddNeighbors(cubes);

            for (var cycle = 0; cycle < 6; cycle++)
            {
                _logger.LogInformation($"Starting cycle {cycle}. {cubes.Values.Count(c => c.Active)} Active cubes");
                var tasks = new List<Task>();
                foreach (var cube in cubes.Values)
                {
                    tasks.Add(Task.Run(() => CheckCubeStateAsync(cube, cubes)));
                }

                Task.WaitAll(tasks.ToArray());
                _logger.LogInformation($"Starting cycle {cycle}. Setting states");
                foreach (var cube in cubes.Values)
                {
                    cube.ApplyNextState();
                }
                _logger.LogInformation($"Starting cycle {cycle}. Setting neighbors");
                AddNeighbors(cubes);
                _logger.LogInformation($"Ending cycle {cycle}. {cubes.Values.Count(c => c.Active)} Active cubes");
            }

            var result = cubes.Values.Count(c => c.Active);

            return $"{result}";
        }

        private async Task CheckCubeStateAsync(Cube cube, Dictionary<string, Cube> cubes)
        {
            var neighbors = cube.GetNeighbors();
            var activeCount = cubes.Values.Count(c => neighbors.Contains(c.Key) && c.Active);
            if (cube.Active && (activeCount < 2 || activeCount > 3))
            {
                cube.ToggleNextState();
            }

            if (!cube.Active && activeCount == 3)
            {
                cube.ToggleNextState();
            }
        }

        private static void AddNeighbors(Dictionary<string, Cube> cubes)
        {
            var newCubes = new Dictionary<string, Cube>();
            foreach (var cube in cubes.Values)
            {
                foreach (var neighbor in cube.GetNeighbors())
                {
                    if (!cubes.ContainsKey(neighbor) && !newCubes.ContainsKey(neighbor))
                    {
                        newCubes.Add(neighbor, new Cube(neighbor));
                    }
                }
            }

            foreach (var newCube in newCubes)
            {
                cubes.Add(newCube.Key, newCube.Value);
            }
        }

        private static Dictionary<string, Cube> ParseInputToDictionary(string[] inputData)
        {
            var cubes = new Dictionary<string, Cube>();
            for (var y = 0; y < inputData.Count(); y++)
            {
                var rowData = inputData[y].ToCharArray();
                for (var x = 0; x < rowData.Length; x++)
                {
                    var cube = new Cube(x, y, 0, 0, rowData[x] == '#');
                    cubes.Add(cube.Key, cube);
                }
            }

            return cubes;
        }

        class Cube
        {
            public Cube(int x, int y, int z, int w, bool active)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
                Active = active;
            }

            public Cube(string key)
            {
                var coordinates = key.Split(';');
                X = int.Parse(coordinates[0]);
                Y = int.Parse(coordinates[1]);
                Z = int.Parse(coordinates[2]);
                W = int.Parse(coordinates[3]);
                Active = false;
            }

            public bool Active { get; set; }
            public int X { get; }
            public int Y { get; }
            public int Z { get; }
            public int W { get; }
            public string Key => $"{X};{Y};{Z};{W}";
            private bool? nextState { get; set; }
            private List<string> neighbors { get; set; }

            public void ToggleNextState()
            {
                nextState = !Active;
            }

            public void ApplyNextState()
            {
                if (nextState != null)
                {
                    Active = nextState.Value;
                }

                nextState = null;
            }

            public IEnumerable<string> GetNeighbors()
            {
                if (neighbors == null)
                {
                    SetNeighbors();
                }

                return neighbors;
            }

            private void SetNeighbors()
            {
                neighbors = new List<string>();
                for (var xDelta = -1; xDelta <= 1; xDelta++)
                {
                    for (var yDelta = -1; yDelta <= 1; yDelta++)
                    {
                        for (var zDelta = -1; zDelta <= 1; zDelta++)
                        {
                            for (var wDelta = -1; wDelta <= 1; wDelta++)
                            {
                                if (!(xDelta == 0 && yDelta == 0 && zDelta == 0 && wDelta == 0))
                                {
                                    neighbors.Add($"{X + xDelta};{Y + yDelta};{Z + zDelta};{W + wDelta}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}