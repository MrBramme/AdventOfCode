using AdventOfCode.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022.Solutions
{
    public class Solution202207Part2 : ISolution
    {
        private Regex regexDirname = new(@"\$ cd (?'dirname'.*)");
        private Regex regexFilesize = new(@"(?'size'^\d+)");
        private readonly ILogger<Solution202207Part2> _logger;
        private readonly IInputService _inputService;
        private readonly string resourceLocation = "Resources2022/Day07.txt";

        public Solution202207Part2(ILogger<Solution202207Part2> logger, IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public string GetSolution()
        {
            var input = _inputService.GetInput(resourceLocation).Where(x => !x.Equals("$ ls") && !x.StartsWith("dir ")).ToList();

            var dirSizes = new Dictionary<string, int>();
            var directories = new Stack<string>();

            foreach (var command in input)
            {
                if (command.Equals("$ cd .."))
                {
                    directories.Pop();
                    continue;
                }

                if (command.Equals("$ cd /"))
                {
                    directories.Clear();
                    directories.Push("/");
                    continue;
                }

                if (command.StartsWith("$ cd "))
                {
                    var dirName = regexDirname.Match(command).Groups["dirname"].Value;
                    directories.Push($"{string.Join("/", directories)}/{dirName}");
                    continue;
                }

                var size = int.Parse(regexFilesize.Match(command).Groups["size"].Value);
                foreach (var dir in directories)
                {
                    if (!dirSizes.ContainsKey(dir))
                    {
                        dirSizes.Add(dir, 0);
                    }
                    dirSizes[dir] += size;
                }

            }
            var freeDiscSpace = 70000000 - dirSizes.Values.Max();
            var result = dirSizes.Values.Where(size => size + freeDiscSpace >= 30000000).Min();
            return $"{result}";
        }
    }
}