using AdventOfCode.Domain.Domain;
using AdventOfCode.Domain.Interfaces;
using AdventOfCode.Year2019;
using AdventOfCode.Year2020;
using AdventOfCode.Year2021;
using System;
using System.Linq;
using System.Reflection;

namespace AdventOfCode.Core.Builder
{
    public class SolutionBuilder : ISolutionBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        public SolutionBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ISolution CreateSolution(Assignment assignment)
        {
            return assignment.Year switch
            {
                2019 => Get2019Solution(assignment),
                2020 => Get2020Solution(assignment),
                2021 => Get2021Solution(assignment),
                _ => throw new ArgumentOutOfRangeException($"No solutions found for year {assignment.Year}")
            };
        }

        private ISolution Get2019Solution(Assignment assignment)
        {
            var solutions2019 = Assembly.GetAssembly(typeof(Assembly2019))
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ISolution)))
                .Where(x => x.IsClass);

            return (ISolution)_serviceProvider.GetService(solutions2019.First(s => s.Name.Equals(GetSolutionName(assignment))));
        }

        private ISolution Get2020Solution(Assignment assignment)
        {
            var solutions2020 = Assembly.GetAssembly(typeof(Assembly2020))
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ISolution)))
                .Where(x => x.IsClass);

            return (ISolution)_serviceProvider.GetService(solutions2020.First(s => s.Name.Equals(GetSolutionName(assignment))));
        }

        private ISolution Get2021Solution(Assignment assignment)
        {
            var solutions2021 = Assembly.GetAssembly(typeof(Assembly2021))
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ISolution)))
                .Where(x => x.IsClass);

            return (ISolution)_serviceProvider.GetService(solutions2021.First(s => s.Name.Equals(GetSolutionName(assignment))));
        }

        private string GetSolutionName(Assignment assignment)
        {
            if (!string.IsNullOrEmpty(assignment.Suffix))
            {
                return $"Solution{assignment.Year}{assignment.Day:00}{assignment.Suffix}";
            }
            return $"Solution{assignment.Year}{assignment.Day:00}";
        }
    }
}
